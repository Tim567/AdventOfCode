namespace AoC.Solutions._2022
{
    public class Day01 : AoCDay
    {
        string[] data;
        List<int> elfVals;

        public Day01() => Seed();

        void Seed()
        {
            data = GetInput();
            int curr = 0;
            elfVals = new List<int>();
            foreach (var line in data)
            {
                if (line == "")
                {
                    elfVals.Add(curr);
                    curr = 0;
                }
                else curr += int.Parse(line);
            }
            elfVals = elfVals.OrderByDescending(e => e).ToList();
        }
        public override void RunPart1() {
            Console.WriteLine("Answer Part 1: " + elfVals[0]);
        }
        public override void RunPart2()
        {
            Console.WriteLine("Answer Part 2: " + (elfVals[0] + elfVals[1] + elfVals[2]));
        }
    }
}
