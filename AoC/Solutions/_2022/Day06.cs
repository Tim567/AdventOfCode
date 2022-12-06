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
        public override void RunPart1()
        {
            List<char> curr = new List<char>();
            int count = 0;
            foreach (var chr in data[0])
            {
                count++;
                //Console.WriteLine(chr);
                curr.Add(chr);
                if (curr.Count >= 4)
                {
                    char[] tmp = curr.Skip(curr.Count - 4).Take(4).ToArray();
                    tmp = tmp.Distinct().ToArray();
                    if (tmp.Count() == 4)
                    {
                        break;
                    }
                }

            }
            Console.WriteLine("Answer Part 1: " + count);
        }

        public override void RunPart2()
        {
            List<char> curr = new List<char>();
            int count = 0;
            foreach (var chr in data[0])
            {
                count++;
                //Console.WriteLine(chr);
                curr.Add(chr);
                if (curr.Count >= 14)
                {
                    char[] tmp = curr.Skip(curr.Count - 14).Take(14).ToArray();
                    tmp = tmp.Distinct().ToArray();
                    if (tmp.Count() == 14)
                    {
                        break;
                    }
                }

            }
            Console.WriteLine("Answer Part 2: " + count);
        }
    }
}
