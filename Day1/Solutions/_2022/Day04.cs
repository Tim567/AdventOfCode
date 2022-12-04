using AoC.Interface;
using System;
namespace AoC.Solutions._2022
{
    public class Day04 : AoCDay
    {
        public override void RunPart1()
        {
            var data = GetInput().Select(line => line.Split(",").Select(line => line.Split("-").Select(line => Convert.ToInt32(line)).ToArray()).ToArray());

            var total = data.Count(line => (line[0][0] <= line[1][0] && line[0][1] >= line[1][1]) || (line[0][0] >= line[1][0] && line[0][1] <= line[1][1]));

            Console.WriteLine("Answer Part 1: " + total);
        }

        public override void RunPart2()
        {
            var data = GetInput().Select(line => line.Split(",").Select(line => line.Split("-").Select(line => Convert.ToInt32(line)).ToArray()).ToArray());

            var total = data.Count(line => line[0][0] <= line[1][1] && line[0][1] >= line[1][0]);

            Console.WriteLine("Answer Part 2: " + total);
        }
    }
}