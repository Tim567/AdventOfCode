using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AoC.Solutions._2024
{
    public class Day03 : AoCDay
    {
        public override void RunPart1()
        {
            string[] data = GetInput();
            var res = 0;
            var d = data.ToOneLongSting();
            var s = d.Split("mul(");
            foreach (var i in s)
            {
                var t = i.Split(",");
                if (!t[0].OnlyDigits()) continue;
                if (!t[1].Contains(")")) continue;
                var k = t[1].Split(")");
                if (!k[0].OnlyDigits()) continue;
                res += Multiply(t[0], k[0]);
            }
            Console.WriteLine(res);
        }

        public override void RunPart2()
        {
            string[] data = GetInput();
            var res = 0;
            var d = data.ToOneLongSting();
            var s = d.Split("mul(");
            foreach (var i in s)
            {
                var t = i.Split(",");
                if (!t[0].OnlyDigits()) continue;
                if (!t[1].Contains(")")) continue;
                var k = t[1].Split(")");
                if (!k[0].OnlyDigits()) continue;

                //find index of match 
                var inds = $"mul({t[0]},{k[0]})";
                var index = d.IndexOf(inds);
                if (Active(d, index))
                    res += Multiply(t[0], k[0]);
            }
            Console.WriteLine(res);
        }

        private int Multiply(string a, string b)
        {
            var intA = int.Parse(a);
            var intB = int.Parse(b);
            return intA.Multiply(intB);
        }

        private List<int> dos = new List<int>();
        private List<int> dons = new List<int>();


        private bool Active(string input, int index)
        {
            if (dos.Count == 0)
            {
                //get all the indexes where do() is pressent
                dos = input.GetAllIndices("do()");
                dons = input.GetAllIndices("don't()");
            }
            var closestDo = dos.Where(x => x < index).LastOrDefault();
            var closestDont = dons.Where(x => x < index).LastOrDefault();
            
            if (closestDo == closestDont) return true;
            var difDo = index - closestDo;
            var difDont = index - closestDont;
            return difDo < difDont;
        }
    }
}
