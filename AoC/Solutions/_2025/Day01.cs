namespace AoC.Solutions._2025
{
    /// <summary>
    /// Example solution demonstrating the testing and automation features
    /// This is a template - replace with actual problem logic
    /// </summary>
    public class Day01 : AoCDay
    {
        public Day01()
        {
            // Example: Add test cases from the problem description
            // These will be run when using the --test flag
            
            // Simple example test case
            AddTestCase(
                input: "1\n2\n3",
                expectedPart1: "6",      // Sum of numbers
                expectedPart2: "12"      // Sum doubled
            );
            
            // You can add multiple test cases
            AddTestCase(
                input: "10\n20\n30",
                expectedPart1: "60",
                expectedPart2: "120"
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
            // Example solution: Sum all numbers
            // Replace this with actual problem logic
            int sum = 0;
            foreach (var line in input)
            {
                if (int.TryParse(line.Trim(), out int num))
                {
                    sum += num;
                }
            }
            return sum.ToString();
        }

        public override string? SolvePart2(string[] input)
        {
            // Example solution: Sum all numbers and double it
            // Replace this with actual problem logic
            int sum = 0;
            foreach (var line in input)
            {
                if (int.TryParse(line.Trim(), out int num))
                {
                    sum += num;
                }
            }
            return (sum * 2).ToString();
        }
    }
}
