# Advent of Code Solutions

A C# template and automation framework for [Advent of Code](https://adventofcode.com/) challenges.

## Features

✨ **Automated Input Fetching** - Automatically downloads puzzle input from adventofcode.com using your session cookie  
🧪 **Built-in Testing** - Test your solutions with sample data before running on real input  
📤 **Automated Submission** - Submit answers directly from the CLI and get instant feedback  
🎯 **Clean Structure** - Organized solution files by year and day  
⚡ **Fast Workflow** - Quick iteration with minimal boilerplate

## Prerequisites

- .NET 6.0 or later
- An Advent of Code account

## Setup

### 1. Clone the Repository

```bash
git clone https://github.com/Tim567/AdventOfCode.git
cd AdventOfCode
```

### 2. Configure Your Session Cookie

Your session cookie is required to fetch puzzle inputs and submit answers. You can configure it in three ways:

#### Option A: Using the CLI (Recommended)
```bash
dotnet run --project AoC -- --configure
```

Then paste your session cookie when prompted.

#### Option B: Manual File Creation
Create a file named `.aoc-session` in the project root and paste your session cookie:
```bash
echo "your_session_cookie_here" > .aoc-session
```

#### Option C: Environment Variable
Set the `AOC_SESSION` environment variable:
```bash
export AOC_SESSION="your_session_cookie_here"
```

### Getting Your Session Cookie

1. Log in to [adventofcode.com](https://adventofcode.com)
2. Open browser DevTools (press F12)
3. Go to **Application** tab (Chrome) or **Storage** tab (Firefox)
4. Navigate to **Cookies** → `https://adventofcode.com`
5. Copy the value of the `session` cookie

**⚠️ Important:** Never commit your session cookie to version control. The `.aoc-session` file is already in `.gitignore`.

## Usage

### Running Solutions

```bash
# Run today's puzzle (uses current date)
dotnet run --project AoC

# Run a specific day
dotnet run --project AoC -- --year 2025 --day 1

# Short form
dotnet run --project AoC -- -y 2025 -d 1
```

### Running with Tests

```bash
# Run with test cases (validates your solution on sample data)
dotnet run --project AoC -- -y 2025 -d 1 --test

# Short form
dotnet run --project AoC -- -y 2025 -d 1 -t
```

### Automated Submission

```bash
# Run with interactive submission prompts
dotnet run --project AoC -- -y 2025 -d 1 --submit

# Run with tests and submission
dotnet run --project AoC -- -y 2025 -d 1 -t -s
```

### All Options

```bash
dotnet run --project AoC -- --help
```

Options:
- `-y, --year <year>` - Specify the year (default: current year)
- `-d, --day <day>` - Specify the day (default: current day)
- `-t, --test` - Run test cases before solving
- `-s, --submit` - Enable interactive answer submission
- `-c, --configure` - Configure session cookie
- `-h, --help` - Show help message

## Creating a New Solution

### Step 1: Create the Solution File

You can use the provided template or create from scratch.

**Option A: Copy the Template**
```bash
# Copy template and rename
cp AoC/Solutions/DayTemplate.cs.template AoC/Solutions/_2025/Day01.cs

# Update:
# - Replace _YYYY with _2025
# - Replace DayDD with Day01
# - Add your solution logic
```

**Option B: Create from Scratch**

Create a new file at `AoC/Solutions/_YYYY/DayDD.cs` (replace YYYY with year, DD with zero-padded day):

```csharp
namespace AoC.Solutions._2025
{
    public class Day01 : AoCDay
    {
        public Day01()
        {
            // Add test cases from the problem description
            AddTestCase(
                input: "sample input from problem",
                expectedPart1: "expected answer for part 1",
                expectedPart2: "expected answer for part 2"
            );
        }

        public override void RunPart1()
        {
            string[] data = GetInput();
            var answer = SolvePart1(data);
            Console.WriteLine($"Answer: {answer}");
        }

        public override void RunPart2()
        {
            string[] data = GetInput();
            var answer = SolvePart2(data);
            Console.WriteLine($"Answer: {answer}");
        }

        public override string? SolvePart1(string[] input)
        {
            // TODO: Implement solution for Part 1
            return null;
        }

        public override string? SolvePart2(string[] input)
        {
            // TODO: Implement solution for Part 2
            return null;
        }
    }
}
```

### Step 2: Run Your Solution

```bash
# This will automatically fetch the input if not present
dotnet run --project AoC -- -y 2025 -d 1 -t
```

The framework will:
1. ✅ Automatically fetch and cache the puzzle input
2. ✅ Run your test cases to validate the solution
3. ✅ Execute your solution on the real input
4. ✅ Optionally submit your answer

## Workflow Example

Here's a typical workflow for solving a new puzzle:

```bash
# 1. Create your solution file (see template above)
#    AoC/Solutions/_2025/Day01.cs

# 2. Run with tests to validate on sample data
dotnet run --project AoC -- -y 2025 -d 1 -t

# 3. Once tests pass, run with submission enabled
dotnet run --project AoC -- -y 2025 -d 1 -s

# 4. The program will prompt you to submit each answer
#    Answer: 42
#    Submit answer '42' for Part 1? (y/n)
```

## Project Structure

```
AdventOfCode/
├── .aoc-session          # Your session cookie (DO NOT COMMIT)
├── AoC/
│   ├── Solutions/
│   │   ├── _2022/        # Solutions for 2022
│   │   ├── _2023/        # Solutions for 2023
│   │   ├── _2024/        # Solutions for 2024
│   │   └── _2025/        # Solutions for 2025
│   ├── Input/
│   │   ├── _2022/        # Cached inputs for 2022
│   │   ├── _2023/        # Cached inputs for 2023
│   │   ├── _2024/        # Cached inputs for 2024
│   │   └── _2025/        # Cached inputs for 2025
│   ├── Services/
│   │   ├── AoCClient.cs  # HTTP client for AoC API
│   │   └── AoCConfig.cs  # Configuration management
│   ├── Interface/
│   │   └── AoCDay.cs     # Base class for solutions
│   └── Program.cs        # CLI entry point
└── README.md
```

## Testing Framework

The framework includes a simple testing system:

```csharp
// In your solution constructor
public Day01()
{
    // Add a test case with expected answers
    AddTestCase(
        input: "sample input",
        expectedPart1: "expected result 1",
        expectedPart2: "expected result 2"
    );
    
    // You can add multiple test cases
    AddTestCase(
        input: "another test",
        expectedPart1: "another result"
        // expectedPart2 is optional
    );
}
```

Run with `-t` flag to execute tests before running on real input.

## Submission Results

When you submit an answer, the framework will:
- ✅ **Correct Answer**: Display success message and unlock the next part
- ❌ **Incorrect Answer**: Show the error message and let you try again
- ⏳ **Rate Limited**: Display wait time before you can submit again
- ℹ️ **Already Completed**: Inform you the puzzle is already solved

## Best Practices

1. **Always test first**: Use the `-t` flag to validate on sample data
2. **Review your answer**: Check the output before submitting
3. **Don't spam submissions**: The API has rate limiting
4. **Keep inputs cached**: The framework caches inputs to avoid unnecessary requests
5. **Never commit secrets**: The `.aoc-session` file is gitignored

## Security

- ⚠️ **Never commit your session cookie** - It's equivalent to your password
- ✅ The `.aoc-session` file is in `.gitignore`
- ✅ Use environment variables on shared machines
- ✅ Treat your session cookie like a password

## Troubleshooting

### "Session cookie not configured"
- Make sure you've run `dotnet run --project AoC -- --configure`
- Or create a `.aoc-session` file with your cookie

### "Day X not available yet"
- The puzzle may not be released yet (puzzles unlock at midnight EST)
- Check the date on adventofcode.com

### "Failed to fetch input"
- Your session cookie may have expired - log in again and get a new one
- Check your internet connection

## Contributing

Feel free to submit issues or pull requests to improve the framework!

## Acknowledgments

- Inspired by [advent-of-code-data](https://github.com/wimglenn/advent-of-code-data)
- Built for [Advent of Code](https://adventofcode.com/) by Eric Wastl

## License

This is a personal solutions repository. The Advent of Code problems and text are the property of [Advent of Code](https://adventofcode.com/).
