namespace AoC.Solutions._2022
{
    public class Day03 : AoCDay
    {
        public override void RunPart1()
        {
            string[] data = GetInput();
            var ans = data.Select(line =>
            {
                var x = line.Substring(0, line.Length / 2);
                var y = line.Substring(line.Length / 2);
                return x.Intersect(y).ToArray()[0];
            })
            .Aggregate(0, (pri, item) => pri + Array.IndexOf(GetAlphabetArray(), item) + 1)
            .ToString();
            Console.WriteLine(ans);
        }

        private char[] GetAlphabetArray() => "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

        public override void RunPart2()
        {
            string[] data = GetInput();

            int index = 0;
            int sum = 0;

            while (index < data.Length)
            {
                var group = data.Skip(index).Take(3);
                var badge = group.IntersectAll().ToArray()[0];
                sum += Array.IndexOf(GetAlphabetArray(), badge) + 1;
                index += 3;
            }

            Console.WriteLine(sum);

        }
    }
}

