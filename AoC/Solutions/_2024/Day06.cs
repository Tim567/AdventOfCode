using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AoC.Solutions._2024
{
    public class Day06 : AoCDay
    {
        public override void RunPart1()
        {
            string[] data = GetInput();
            HashSet<(int, int)> seen = new();
            (int, int) location = (0, 0);
            for (int i = 0; i < data.Length; i++)
            {
                string row = data[i];
                for (int j = 0; j < row.Length; j++)
                {
                    var c = row[j];
                    if (c == '^')
                    {
                        location = (i, j);
                    }
                }
            }
            // Directions: up, right, down, left
            (int, int)[] directions = { (-1, 0), (0, 1), (1, 0), (0, -1) };
            int d = 0;
            seen.Add(location);
            while (true)
            {
                var dir = directions[d % 4];
                var next = (location.Item1 + dir.Item1, location.Item2 + dir.Item2);
                if (next.Item1 < 0 || next.Item2 < 0)
                    break;
                if (next.Item1 >= data.Length || next.Item2 >= data[0].Length)
                    break;

                var item = data[next.Item1][next.Item2];
                if (item == '#')
                {
                    d += 1;
                }
                else
                {
                    seen.Add(next);
                    location = next;
                }
            }
            Console.WriteLine(seen.Count);
        }

        public override void RunPart2()
        {
            var res = 0;
            string[] data = GetInput();
            HashSet<((int, int), int)> seen = new(); //(location), direction
            (int, int) start = (0, 0);
            for (int i = 0; i < data.Length; i++)
            {
                string row = data[i];
                for (int j = 0; j < row.Length; j++)
                {
                    var c = row[j];
                    if (c == '^')
                    {
                        start = (i, j);
                    }
                }
            }
            // Directions: up, right, down, left
            (int, int)[] directions = { (-1, 0), (0, 1), (1, 0), (0, -1) };

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    int d = 0;
                    var location = (start.Item1, start.Item2);
                    seen.Clear();
                    seen.Add((location, d));

                    while (true)
                    {
                        var dir = directions[d % 4];
                        var next = (location.Item1 + dir.Item1, location.Item2 + dir.Item2);
                        if (next.Item1 < 0 || next.Item2 < 0)
                            break;
                        if (next.Item1 >= data.Length || next.Item2 >= data[0].Length)
                            break;

                        var item = data[next.Item1][next.Item2];
                        if (item == '#' || (next.Item1 == i && next.Item2 == j))
                        {
                            d += 1;
                            d = d % 4;
                        }
                        else
                        {
                            if (seen.Contains((next, d)))
                            {
                                res += 1;
                                break;
                            }
                            seen.Add((next, d));
                            location = next;
                        }
                    }
                }
            }
            Console.WriteLine(res);
        }
    }
}