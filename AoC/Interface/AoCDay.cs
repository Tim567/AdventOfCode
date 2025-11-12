using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AoC.Services;

namespace AoC.Interface
{
    public abstract class AoCDay
    {
        protected List<TestCase> TestCases { get; } = new List<TestCase>();

        public string[] GetInput()
        {
            var year = int.Parse(GetType().Namespace![^4..]);
            var day = int.Parse(GetType().Name.Replace("Day", ""));
            
            // Use paths relative to the executable location
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var inputPath = Path.Combine(baseDir, "Input", $"_{year}", $"{day:D2}.txt");
            var sourceInputPath = Path.Combine(baseDir, "..", "..", "..", "Input", $"_{year}", $"{day:D2}.txt");

            // Check if input file exists and has content
            if (File.Exists(inputPath) && new FileInfo(inputPath).Length > 0)
            {
                return File.ReadAllLines(inputPath);
            }

            // Try automated fetch if session cookie is configured
            if (AoCConfig.HasSessionCookie())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Input file not found. Attempting to fetch from adventofcode.com...");
                Console.ForegroundColor = ConsoleColor.White;
                
                var client = new AoCClient();
                var input = client.FetchInputAsync(year, day).GetAwaiter().GetResult();
                
                if (!string.IsNullOrEmpty(input))
                {
                    // Ensure directory exists
                    Directory.CreateDirectory(Path.GetDirectoryName(inputPath)!);
                    Directory.CreateDirectory(Path.GetDirectoryName(sourceInputPath)!);
                    
                    // Save to both locations
                    var lines = input.Split('\n');
                    File.WriteAllLines(inputPath, lines);
                    File.WriteAllLines(sourceInputPath, lines);
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"✓ Input saved to: {inputPath}");
                    Console.ForegroundColor = ConsoleColor.White;
                    return lines;
                }
            }

            // Fallback to clipboard method
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("No input found!");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Use clipboard to set input? (y/n)");
            Console.ForegroundColor = ConsoleColor.White;
            if (Console.ReadKey() is ConsoleKeyInfo key && key.Key == ConsoleKey.Y)
            {
                var input = GetText();
                Directory.CreateDirectory(Path.GetDirectoryName(inputPath)!);
                Directory.CreateDirectory(Path.GetDirectoryName(sourceInputPath)!);
                File.WriteAllLines(inputPath, input.Split(Environment.NewLine));
                File.WriteAllLines(sourceInputPath, input.Split(Environment.NewLine));
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Input saved to file: {inputPath}");
                Console.ForegroundColor = ConsoleColor.White;
                return input.Split(Environment.NewLine);
            }

            return Array.Empty<string>();
        }

        public abstract void RunPart1();
        public abstract void RunPart2();

        // Optional: Override these for automated testing
        public virtual string? SolvePart1(string[] input) => null;
        public virtual string? SolvePart2(string[] input) => null;

        protected void AddTestCase(string input, string? expectedPart1 = null, string? expectedPart2 = null)
        {
            TestCases.Add(new TestCase
            {
                Input = input.Split('\n'),
                ExpectedPart1 = expectedPart1,
                ExpectedPart2 = expectedPart2
            });
        }

        public bool RunTests(int part)
        {
            if (TestCases.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No test cases defined.");
                Console.ForegroundColor = ConsoleColor.White;
                return true;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\n▶ Running {TestCases.Count} test case(s) for Part {part}...");
            Console.ForegroundColor = ConsoleColor.White;

            bool allPassed = true;
            for (int i = 0; i < TestCases.Count; i++)
            {
                var testCase = TestCases[i];
                var expected = part == 1 ? testCase.ExpectedPart1 : testCase.ExpectedPart2;
                
                if (expected == null)
                    continue;

                var result = part == 1 ? SolvePart1(testCase.Input) : SolvePart2(testCase.Input);
                
                if (result == expected)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"  ✓ Test {i + 1} passed: {result}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"  ✗ Test {i + 1} failed:");
                    Console.WriteLine($"    Expected: {expected}");
                    Console.WriteLine($"    Got:      {result}");
                    Console.ForegroundColor = ConsoleColor.White;
                    allPassed = false;
                }
            }

            return allPassed;
        }

        public async Task<bool> SubmitAnswer(int part, string answer)
        {
            if (!AoCConfig.HasSessionCookie())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot submit: Session cookie not configured.");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }

            var year = int.Parse(GetType().Namespace![^4..]);
            var day = int.Parse(GetType().Name.Replace("Day", ""));

            var client = new AoCClient();
            var result = await client.SubmitAnswerAsync(year, day, part, answer);

            if (result.Success)
            {
                if (result.IsCorrect)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n{result.Message}");
                    Console.ForegroundColor = ConsoleColor.White;
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{result.Message}");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\n{result.Message}");
                if (result.WaitTimeSeconds.HasValue)
                {
                    Console.WriteLine($"Please wait {result.WaitTimeSeconds} seconds before trying again.");
                }
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
        }

        private static string GetText()
        {
            var powershell = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    RedirectStandardOutput = true,
                    FileName = "powershell",
                    Arguments = "-command \"Get-Clipboard\""
                }
            };

            powershell.Start();
            string text = powershell.StandardOutput.ReadToEnd();
            powershell.StandardOutput.Close();
            powershell.WaitForExit();
            return text.TrimEnd();
        }
    }

    public class TestCase
    {
        public string[] Input { get; set; } = Array.Empty<string>();
        public string? ExpectedPart1 { get; set; }
        public string? ExpectedPart2 { get; set; }
    }
}
