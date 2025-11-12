using System.Net;
using System.Text.RegularExpressions;

namespace AoC.Services
{
    public class AoCClient
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://adventofcode.com";

        public AoCClient()
        {
            var handler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "github.com/Tim567/AdventOfCode via .NET");
        }

        public async Task<string?> FetchInputAsync(int year, int day)
        {
            var sessionCookie = AoCConfig.GetSessionCookie();
            if (string.IsNullOrWhiteSpace(sessionCookie))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Session cookie not configured!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Please set your session cookie using one of these methods:");
                Console.WriteLine("1. Create a .aoc-session file in the project root");
                Console.WriteLine("2. Set the AOC_SESSION environment variable");
                Console.WriteLine("\nTo get your session cookie:");
                Console.WriteLine("1. Log in to https://adventofcode.com");
                Console.WriteLine("2. Open browser DevTools (F12)");
                Console.WriteLine("3. Go to Application/Storage > Cookies");
                Console.WriteLine("4. Copy the value of the 'session' cookie");
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }

            try
            {
                var url = $"{BaseUrl}/{year}/day/{day}/input";
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Cookie", $"session={sessionCookie}");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Fetching input from: {url}");
                Console.ForegroundColor = ConsoleColor.White;

                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: Day {day} of year {year} not available yet or doesn't exist.");
                    Console.ForegroundColor = ConsoleColor.White;
                    return null;
                }

                if (!response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error: Failed to fetch input. Status: {response.StatusCode}");
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        Console.WriteLine("Check if your session cookie is valid.");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✓ Successfully fetched input ({content.Length} characters)");
                Console.ForegroundColor = ConsoleColor.White;
                return content.TrimEnd();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error fetching input: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
        }

        public async Task<SubmissionResult> SubmitAnswerAsync(int year, int day, int part, string answer)
        {
            var sessionCookie = AoCConfig.GetSessionCookie();
            if (string.IsNullOrWhiteSpace(sessionCookie))
            {
                return new SubmissionResult
                {
                    Success = false,
                    Message = "Session cookie not configured. Cannot submit answer."
                };
            }

            try
            {
                var url = $"{BaseUrl}/{year}/day/{day}/answer";
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("Cookie", $"session={sessionCookie}");

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("level", part.ToString()),
                    new KeyValuePair<string, string>("answer", answer)
                });
                request.Content = content;

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Submitting answer for {year} day {day} part {part}: {answer}");
                Console.ForegroundColor = ConsoleColor.White;

                var response = await _httpClient.SendAsync(request);
                var responseText = await response.Content.ReadAsStringAsync();

                return ParseSubmissionResponse(responseText);
            }
            catch (Exception ex)
            {
                return new SubmissionResult
                {
                    Success = false,
                    Message = $"Error submitting answer: {ex.Message}"
                };
            }
        }

        private SubmissionResult ParseSubmissionResponse(string html)
        {
            var result = new SubmissionResult();

            // Extract the main message from the response
            var match = Regex.Match(html, @"<article><p>(.*?)</p></article>", RegexOptions.Singleline);
            if (match.Success)
            {
                var message = match.Groups[1].Value;
                // Remove HTML tags
                message = Regex.Replace(message, @"<[^>]+>", "");
                // Decode HTML entities
                message = WebUtility.HtmlDecode(message);
                result.Message = message.Trim();

                // Check for specific responses
                if (message.Contains("That's the right answer", StringComparison.OrdinalIgnoreCase))
                {
                    result.Success = true;
                    result.IsCorrect = true;
                    result.Message = "✓ Correct! " + message;
                }
                else if (message.Contains("That's not the right answer", StringComparison.OrdinalIgnoreCase))
                {
                    result.Success = true;
                    result.IsCorrect = false;
                    result.Message = "✗ Incorrect. " + message;
                }
                else if (message.Contains("You gave an answer too recently", StringComparison.OrdinalIgnoreCase))
                {
                    result.Success = false;
                    result.Message = "⏳ Rate limited. " + message;
                    
                    // Try to extract wait time
                    var waitMatch = Regex.Match(message, @"(\d+)s");
                    if (waitMatch.Success)
                    {
                        result.WaitTimeSeconds = int.Parse(waitMatch.Groups[1].Value);
                    }
                    else
                    {
                        waitMatch = Regex.Match(message, @"(\d+)m");
                        if (waitMatch.Success)
                        {
                            result.WaitTimeSeconds = int.Parse(waitMatch.Groups[1].Value) * 60;
                        }
                    }
                }
                else if (message.Contains("Did you already complete it", StringComparison.OrdinalIgnoreCase))
                {
                    result.Success = false;
                    result.Message = "Already completed.";
                }
                else
                {
                    result.Success = true;
                }
            }
            else
            {
                result.Success = false;
                result.Message = "Could not parse response from server.";
            }

            return result;
        }
    }

    public class SubmissionResult
    {
        public bool Success { get; set; }
        public bool IsCorrect { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? WaitTimeSeconds { get; set; }
    }
}
