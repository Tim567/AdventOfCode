using Microsoft.VisualBasic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AoC.Solutions._2024
{
    public class Day05 : AoCDay
    {
        //private KeyValuePair<int, int> DepentsOn = new();
        List<(int, int)> DepentsOn = new();
        public override void RunPart1()
        {
            string[] data = GetInput();
            var dataAbove = data.DataAboveLine("");
            var dataBelow = data.DataBelowLine("");
            var res = 0;

            foreach (var d in dataAbove)
            {
                var ints = d.Split("|");
                var intl = ints[0].ToInt();
                var intr = ints[1].ToInt();
                DepentsOn.Add((intr, intl));
            }

            foreach (var d in dataBelow)
            {
                if (Valid(d))
                {
                    var ints = d.ToIntArray(",");
                    res += ints[(ints.Length / 2)];
                }
            }

            Console.WriteLine(res);
        }

        public override void RunPart2()
        {
            string[] data = GetInput();
            var dataAbove = data.DataAboveLine("");
            var dataBelow = data.DataBelowLine("");
            var res = 0;

            foreach (var d in dataAbove)
            {
                var ints = d.Split("|");
                var intl = ints[0].ToInt();
                var intr = ints[1].ToInt();
                DepentsOn.Add((intr, intl));
            }

            foreach (var d in dataBelow)
            {
                if (!Valid(d))
                {
                    var ints = d.ToIntArray(",");
                    var resu = MakeValid(ints);
                    res += resu[(resu.Length / 2)];
                }
            }

            Console.WriteLine(res);
        }


        private bool Valid(string input)
        {
            List<int> done = new List<int>();
            var ints = input.ToIntArray(",");
            foreach (var i in ints)
            {
                //var dep = DepentsOn[iint];
                var matchingRecords = DepentsOn.Where(record => record.Item1 == i).ToList();

                foreach (var record in matchingRecords)
                {
                    if (!done.Contains(record.Item2) && ints.Contains(record.Item2))
                    {
                        return false;
                    }
                }

                done.Add(i);
            }
            return true;
        }

        private int[] MakeValid(int[] input)
        {
            var graph = new Dictionary<int, List<int>>();
            var inDegree = new Dictionary<int, int>();

            // Build the graph
            foreach (var i in input)
            {
                graph[i] = new List<int>();
                inDegree[i] = 0;
            }

            foreach (var rule in DepentsOn)
            {
                if (graph.ContainsKey(rule.Item1) && graph.ContainsKey(rule.Item2))
                {
                    graph[rule.Item1].Add(rule.Item2);
                    inDegree[rule.Item2]++;
                }
            }

            // Perform sort
            var queue = new Queue<int>(inDegree.Where(kvp => kvp.Value == 0).Select(kvp => kvp.Key));
            var result = new List<int>();

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                result.Add(current);

                foreach (var neighbor in graph[current])
                {
                    inDegree[neighbor]--;
                    if (inDegree[neighbor] == 0)
                    {
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return result.ToArray();
        }
    }
}
