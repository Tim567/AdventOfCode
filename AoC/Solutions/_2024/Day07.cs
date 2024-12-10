using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AoC.Solutions._2024
{
    public class Day07 : AoCDay
    {
        public override void RunPart1()
        {
            long res = 0;
            string[] data = GetInput();
            foreach (string d in data)
            {
                var p = d.Split(": ");
                var target = p[0].ToLong();
                var ns = p[1].ToLongArray(" ");
                if (IsValid(target, ns.ToList()))
                {
                    res+= target; 
                }
            }

            Console.WriteLine(res);
        }

        public override void RunPart2()
        {
            long res = 0;
            string[] data = GetInput();
            foreach (string d in data)
            {
                var p = d.Split(": ");
                var target = p[0].ToLong();
                var ns = p[1].ToLongArray(" ");
                if (IsValid(target, ns.ToList(), true))
                {
                    res += target;
                }
            }

            Console.WriteLine(res);
        }

        private bool IsValid(long target, List<long> ns, bool p2 = false)
        {
            if (ns.Count == 1)
                return ns[0] == target;

            var nsAdd = new List<long>(ns);
            nsAdd[0] = ns[0] + ns[1];
            nsAdd.RemoveAt(1);
            if (IsValid(target, nsAdd, p2))
                return true;

            var nsMultiply = new List<long>(ns);
            nsMultiply[0] = ns[0] * ns[1];
            nsMultiply.RemoveAt(1);
            if (IsValid(target, nsMultiply, p2))
                return true;

            if (p2)
            {
                var nsConcat = new List<long>(ns);
                nsConcat[0] = (ns[0].ToString() + ns[1].ToString()).ToLong();
                nsConcat.RemoveAt(1);
                if (IsValid(target, nsConcat, p2))
                {
                    return true;
                }
            }

            return false;
        }
    }
}