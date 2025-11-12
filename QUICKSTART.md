# Quick Start Guide

Get started with Advent of Code 2025 in under 5 minutes!

## 1. Configure Your Session Cookie

Choose one of these methods:

### Option A: Interactive (Easiest)
```bash
dotnet run --project AoC -- --configure
```
Then paste your session cookie when prompted.

### Option B: Create File Manually
```bash
echo "your_session_cookie_here" > .aoc-session
```

### Option C: Environment Variable
```bash
export AOC_SESSION="your_session_cookie_here"
```

**To get your session cookie:**
1. Log in to [adventofcode.com](https://adventofcode.com)
2. Open browser DevTools (F12)
3. Go to Application → Cookies → `https://adventofcode.com`
4. Copy the value of the `session` cookie

## 2. Create Your Solution

Create a new file `AoC/Solutions/_2025/DayXX.cs`:

```csharp
namespace AoC.Solutions._2025
{
    public class Day01 : AoCDay
    {
        public Day01()
        {
            // Add test cases from the problem
            AddTestCase(
                input: "sample input",
                expectedPart1: "expected answer 1",
                expectedPart2: "expected answer 2"
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
            // Your solution here
            return null;
        }

        public override string? SolvePart2(string[] input)
        {
            // Your solution here
            return null;
        }
    }
}
```

## 3. Run Your Solution

```bash
# With tests (recommended)
dotnet run --project AoC -- -y 2025 -d 1 -t

# With submission
dotnet run --project AoC -- -y 2025 -d 1 -t -s
```

The framework will:
1. ✅ Run your test cases
2. ✅ Fetch the input automatically (if not cached)
3. ✅ Execute your solution
4. ✅ Optionally submit your answer

## Common Commands

```bash
# Run today's puzzle
dotnet run --project AoC

# Run specific day
dotnet run --project AoC -- -y 2025 -d 1

# Run with tests
dotnet run --project AoC -- -y 2025 -d 1 -t

# Run with tests and submission
dotnet run --project AoC -- -y 2025 -d 1 -t -s

# Show help
dotnet run --project AoC -- --help
```

## Tips

- **Always test first**: Use `-t` to validate on sample data
- **Input is cached**: Once fetched, input is saved locally
- **Tests are optional**: But highly recommended!
- **Submission is interactive**: You'll be prompted before submitting

## What Happens When You Run?

1. **Tests Run** (if `-t` flag): Validates your solution on sample data
2. **Input Fetching**: 
   - Checks local cache first
   - Downloads from adventofcode.com if not found
   - Saves to `AoC/Input/_YYYY/DD.txt`
3. **Solution Execution**: Runs your code on the real input
4. **Submission** (if `-s` flag): 
   - Prompts you to confirm
   - Submits to adventofcode.com
   - Shows result (correct/incorrect/rate limited)

## Troubleshooting

### "Session cookie not configured"
→ Run `dotnet run --project AoC -- --configure`

### "No input found"
→ Make sure your session cookie is valid

### Tests fail
→ Check your solution logic against the sample input

### "Day X not available yet"
→ Puzzles unlock at midnight EST

## Next Steps

See the full [README.md](README.md) for:
- Detailed API documentation
- Advanced usage examples
- Security best practices
- Project structure details

Happy coding! 🎄⭐
