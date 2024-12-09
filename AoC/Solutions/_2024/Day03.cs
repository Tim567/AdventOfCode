namespace AoC.Solutions._2024
{
    public class Day03 : AoCDay
    {
        public override void RunPart1()
        {
            string[] data = GetInput();
            var res = 0;
            var d = data[0];
            var s = d.Split("mul(");
            foreach (var i in s)
            {
                var t = i.Split(",");
                if (!t[0].OnlyDigits()) continue;
                var k = t[1].Split(")");
                if (!k[0].OnlyDigits()) continue;
                res += Multiply(int.Parse(t[0]), int.Parse(k[0]));
            }
            Console.WriteLine(res);
        }

        public override void RunPart2()
        {
            string[] data = GetInput();
            var res = 0;
            Console.WriteLine(res);
        }

        private int Multiply(int a, int b)
        {
            return a.Multiply(b);
        }
    }
}
