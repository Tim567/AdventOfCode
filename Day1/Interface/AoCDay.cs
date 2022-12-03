using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Interface
{
    public abstract class AoCDay
    {
        public string[] GetInput() => File.ReadAllLines($"Input/{GetType().Namespace[^5..]}/{GetType().Name.Replace("Day", "")}.txt");
        public abstract void RunPart1();
        public abstract void RunPart2();
    }
}
