namespace AoC.Solutions._2022
{
    public class Day10 : AoCDay
    {
        string[] data;
        public Day10() => Seed();

        void Seed()
        {
            data = GetInput();

        }

        public override void RunPart1()
        {
            var ans = 0;
            var cycles = new List<int> { 20, 60, 100, 140, 180, 220 };
            var X = 1;
            var cycle = 0;

            foreach (var line in data)
            {
                if (line.StartsWith("noop"))
                {
                    cycle++;
                    // if cylce is in cycles
                    if (cycles.Contains(cycle))
                        ans += (X * cycle);
                }
                else if (line.StartsWith("addx"))
                {
                    int i = 2;
                    while (i > 0)
                    {
                        cycle++;
                        if (cycles.Contains(cycle))
                            ans += (X * cycle);
                        i--;
                    }
                    X += int.Parse(line.Split(' ')[1]);
                }
            }


            Console.WriteLine("Answer Part 1: " + ans);
        }

        public override void RunPart2()
        {
            int X = 1;
            string row = "";
            List<string> display = new List<string>();
            foreach (string d in data)
            {
                if (d.StartsWith("noop"))
                {
                    row += (Enumerable.Range(X - 1, X + 2).Contains(row.Length) ? "#" : ".");
                    if (row.Length == 40)
                    {
                        display.Add(row);
                        row = "";
                    }
                }
                if (d.StartsWith("addx"))
                {
                    int i = 2;
                    while (i > 0)
                    {
                        row += (Enumerable.Range(X - 1, X + 2).Contains(row.Length) ? "#" : ".");
                        if (row.Length == 40)
                        {
                            display.Add(row);
                            row = "";
                        }
                        i -= 1;
                    }
                    X += int.Parse(d.Split()[1]);
                }
            }
            Console.WriteLine("Answer Part 2: ");
            Console.WriteLine(string.Join("\n", display));
        }
    }
}
