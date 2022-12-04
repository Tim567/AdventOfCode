namespace AoC.Solutions._2022
{
    public class Day01 : AoCDay
    {
        public override void RunPart1() { }
        public override void RunPart2()
        {
            string[] data = GetInput();
            int curr = 0;
            int max = 0;

            int[] maxThree = new int[3];

            foreach (var line in data)
            {
                if (line == "")
                {
                    if (curr > max) max = curr;
                    if (curr > maxThree[0])
                    {
                        maxThree[2] = maxThree[1];
                        maxThree[1] = maxThree[0];
                        maxThree[0] = curr;
                    }
                    else if (curr > maxThree[1])
                    {
                        maxThree[2] = maxThree[1];
                        maxThree[1] = curr;
                    }
                    else if (curr > maxThree[2])
                    {
                        maxThree[2] = curr;
                    }
                    curr = 0;
                }
                else curr += int.Parse(line);
            }
            Console.WriteLine(max);
            Console.WriteLine(maxThree[0] + maxThree[1] + maxThree[2]);
        }
    }
}
