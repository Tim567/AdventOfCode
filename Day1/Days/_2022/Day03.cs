using Day1.Interface;
using Day1.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1.Days._2022
{
    public class Day03 : IAoCDay
    {
        public string[] GetInput() => File.ReadAllLines($"Input/_{DateTime.Now.Year}/{this.GetType().Name.Replace("Day", "")}.txt");

        public void RunPart1()
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

        public void RunPart2()
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

