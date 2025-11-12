using AoC.Services;

// Parse command line arguments
int? argYear = null;
int? argDay = null;
bool runTests = false;
bool autoSubmit = false;
bool configureSession = false;

for (int i = 0; i < args.Length; i++)
{
    if (args[i] == "--year" || args[i] == "-y")
    {
        if (i + 1 < args.Length && int.TryParse(args[i + 1], out int parsedYear))
        {
            argYear = parsedYear;
            i++;
        }
    }
    else if (args[i] == "--day" || args[i] == "-d")
    {
        if (i + 1 < args.Length && int.TryParse(args[i + 1], out int parsedDay))
        {
            argDay = parsedDay;
            i++;
        }
    }
    else if (args[i] == "--test" || args[i] == "-t")
    {
        runTests = true;
    }
    else if (args[i] == "--submit" || args[i] == "-s")
    {
        autoSubmit = true;
    }
    else if (args[i] == "--configure" || args[i] == "-c")
    {
        configureSession = true;
    }
    else if (args[i] == "--help" || args[i] == "-h")
    {
        ShowHelp();
        return;
    }
}

// Handle session configuration
if (configureSession)
{
    ConfigureSession();
    return;
}

// Default to current date
string year = (argYear ?? DateTime.Now.Year).ToString();
string day = (argDay ?? DateTime.Today.Day).ToString("00");

// For testing purposes, you can uncomment this to test a specific day:
// day = 1.ToString("00");

try
{
    AoCDay? dayClass = Activator.CreateInstance(Type.GetType($"AoC.Solutions._{year}.Day{day}")) as AoCDay;
    
    if (dayClass == null)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error: Could not find solution class for year {year}, day {day}");
        Console.WriteLine($"Make sure AoC.Solutions._{year}.Day{day} exists.");
        Console.ForegroundColor = ConsoleColor.White;
        return;
    }
    
    RunDay(dayClass, year, day, runTests, autoSubmit);
}
catch (Exception ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Error: {ex.Message}");
    Console.ForegroundColor = ConsoleColor.White;
    return;
}

Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("\nRun previous days? (y/n)");
Console.ForegroundColor = ConsoleColor.White;
if (Console.ReadKey() is ConsoleKeyInfo key && key.Key == ConsoleKey.Y)
{
    Console.SetCursorPosition(0, Console.CursorTop - 1);
    Console.WriteLine();
    day = (int.Parse(day) - 1).ToString("00");
    while (day != "00")
    {
        try
        {
            AoCDay? dayClass = Activator.CreateInstance(Type.GetType($"AoC.Solutions._{year}.Day{day}")) as AoCDay;
            if (dayClass != null)
            {
                RunDay(dayClass, year, day, false, false);
            }
        }
        catch
        {
            // Skip days that don't exist
        }
        day = (int.Parse(day) - 1).ToString("00");
        Thread.Sleep(300);
    }
}

static void RunDay(AoCDay dayClass, string year, string day, bool runTests, bool autoSubmit)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("+----------------------------+");
    Console.WriteLine($"| Advent of Code {year} Day {day} |");
    Console.WriteLine("+----------------------------+");
    Console.ForegroundColor = ConsoleColor.White;

    // Run tests if requested
    if (runTests)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Part 1 Tests:");
        Console.ForegroundColor = ConsoleColor.White;
        bool part1TestsPassed = dayClass.RunTests(1);
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nPart 2 Tests:");
        Console.ForegroundColor = ConsoleColor.White;
        bool part2TestsPassed = dayClass.RunTests(2);
        
        if (!part1TestsPassed || !part2TestsPassed)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nSome tests failed. Do you want to continue? (y/n)");
            Console.ForegroundColor = ConsoleColor.White;
            if (Console.ReadKey().Key != ConsoleKey.Y)
            {
                Console.WriteLine();
                return;
            }
            Console.WriteLine();
        }
    }

    // Run Part 1
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\nPart 1:");
    Console.ForegroundColor = ConsoleColor.White;
    dayClass.RunPart1();

    // Ask about submission for Part 1
    if (autoSubmit)
    {
        string[] input = dayClass.GetInput();
        var answer = dayClass.SolvePart1(input);
        if (!string.IsNullOrEmpty(answer))
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"\nSubmit answer '{answer}' for Part 1? (y/n) ");
            Console.ForegroundColor = ConsoleColor.White;
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                dayClass.SubmitAnswer(1, answer).Wait();
            }
            Console.WriteLine();
        }
    }

    // Run Part 2
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\nPart 2:");
    Console.ForegroundColor = ConsoleColor.White;
    dayClass.RunPart2();

    // Ask about submission for Part 2
    if (autoSubmit)
    {
        string[] input = dayClass.GetInput();
        var answer = dayClass.SolvePart2(input);
        if (!string.IsNullOrEmpty(answer))
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write($"\nSubmit answer '{answer}' for Part 2? (y/n) ");
            Console.ForegroundColor = ConsoleColor.White;
            if (Console.ReadKey().Key == ConsoleKey.Y)
            {
                Console.WriteLine();
                dayClass.SubmitAnswer(2, answer).Wait();
            }
            Console.WriteLine();
        }
    }
}

static void ConfigureSession()
{
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("=== Advent of Code Session Configuration ===");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("\nTo get your session cookie:");
    Console.WriteLine("1. Log in to https://adventofcode.com");
    Console.WriteLine("2. Open browser DevTools (F12)");
    Console.WriteLine("3. Go to Application/Storage > Cookies");
    Console.WriteLine("4. Copy the value of the 'session' cookie");
    Console.WriteLine("\nPaste your session cookie:");
    
    string? sessionCookie = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(sessionCookie))
    {
        AoCConfig.SetSessionCookie(sessionCookie.Trim());
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("✓ Session cookie saved successfully!");
        Console.ForegroundColor = ConsoleColor.White;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error: No session cookie provided.");
        Console.ForegroundColor = ConsoleColor.White;
    }
}

static void ShowHelp()
{
    Console.WriteLine("Advent of Code Solution Runner");
    Console.WriteLine("\nUsage: AoC [options]");
    Console.WriteLine("\nOptions:");
    Console.WriteLine("  -y, --year <year>     Specify the year (default: current year)");
    Console.WriteLine("  -d, --day <day>       Specify the day (default: current day)");
    Console.WriteLine("  -t, --test            Run test cases before solving");
    Console.WriteLine("  -s, --submit          Enable interactive answer submission");
    Console.WriteLine("  -c, --configure       Configure session cookie");
    Console.WriteLine("  -h, --help            Show this help message");
    Console.WriteLine("\nExamples:");
    Console.WriteLine("  AoC                              # Run today's puzzle");
    Console.WriteLine("  AoC -y 2025 -d 1                # Run 2025 day 1");
    Console.WriteLine("  AoC -y 2025 -d 1 -t             # Run with tests");
    Console.WriteLine("  AoC -y 2025 -d 1 -t -s          # Run with tests and submission");
    Console.WriteLine("  AoC --configure                  # Set up session cookie");
    Console.WriteLine("\nSession Cookie:");
    Console.WriteLine("  The session cookie can be configured using:");
    Console.WriteLine("  1. Running: AoC --configure");
    Console.WriteLine("  2. Creating a .aoc-session file in the project root");
    Console.WriteLine("  3. Setting the AOC_SESSION environment variable");
}
