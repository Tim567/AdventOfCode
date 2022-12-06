namespace AoC.Solutions._2022
{
    public class Day06 : AoCDay
    {
        string[] data;

        public Day06() => Seed();

        void Seed()
        {
            data = GetInput();
        }
        public override void RunPart1() => Console.WriteLine("Answer Part 1: " + GetFirstIndexUniquePair(4));

        public override void RunPart2() => Console.WriteLine("Answer Part 2: " + GetFirstIndexUniquePair(14));

        int GetFirstIndexUniquePair(int len)
        {
            List<char> curr = new List<char>();
            int count = 0;
            foreach (var chr in data[0])
            {
                count++;
                curr.Add(chr);
                if (curr.Count >= len)
                {
                    char[] tmp = curr.Skip(curr.Count - len).Take(len).ToArray();
                    tmp = tmp.Distinct().ToArray();
                    if (tmp.Count() == len)
                    {
                        break;
                    }
                }

            }
            return count;
        }
    }
}
