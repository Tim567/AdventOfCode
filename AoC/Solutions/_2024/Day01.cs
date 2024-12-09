namespace AoC.Solutions._2024
{
    public class Day01 : AoCDay
    {
        public override void RunPart1()
        {
            string[] data = GetInput();
            List<int> l = new();
            List<int> r = new();
            foreach (string s in data)
            {
                var nbrs = s.Split("  ");
                l.Add(int.Parse(nbrs[0]));
                r.Add(int.Parse(nbrs[1]));
            }
            //order the lists
            l = l.OrderBy(x => x).ToList();
            r = r.OrderBy(x => x).ToList();

            int res = 0;
            for (int i = 0; i < l.Count; i++)
            {
                var tmp = l[i] - r[i];
                res += tmp < 0 ? tmp * -1 : tmp;
            }
            Console.WriteLine(res);
        }

        public override void RunPart2()
        {
            string[] data = GetInput();
            int res = 0;
            List<int> l = new();
            List<int> r = new();
            foreach (string s in data)
            {
                var nbrs = s.Split("  ");
                l.Add(int.Parse(nbrs[0]));
                r.Add(int.Parse(nbrs[1]));
            }

            for (int i = 0; i < l.Count; i++)
            {
                var s = l[i];
                var t = r.FindAll(x => x == s).Count;
                res += s * t;
            }

            Console.WriteLine(res);
        }
    }
}
