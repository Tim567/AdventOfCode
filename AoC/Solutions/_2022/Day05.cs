using System.Net;

namespace AoC.Solutions._2022
{
    public class Day05 : AoCDay
    {
        string[] data;
        List<Stack<char>> stacks;
        string[] movements;
        List<(int cnt, int src, int dest)> instructions;
        public Day05() => Seed();

        private void Seed()
        {
            data = GetInput();
            stacks = ParseInput(data);
            movements = data.Skip(10).ToArray();
            instructions = new();
            foreach (var move in movements)
            {
                var tokens = move.Split(" ");
                instructions.Add((int.Parse(tokens[1]), int.Parse(tokens[3]), int.Parse(tokens[^1])));
            }
        }

        public override void RunPart1()
        {
            foreach (var ins in instructions) for (int i = 0; i < ins.cnt; i++) stacks[ins.dest - 1].Push(stacks[ins.src - 1].Pop());
            StringBuilder sb = new();
            foreach (var s in stacks) sb.Append(s.Peek());
            Console.WriteLine("Answer Part 1: " + sb);
        }

        public override void RunPart2()
        {
            Seed();
            foreach (var ins in instructions)
            {
                Stack<char> tmp = new Stack<char>();
                for (int i = 0; i < ins.cnt; i++) tmp.Push(stacks[ins.src - 1].Pop());
                while (tmp.Count > 0) stacks[ins.dest - 1].Push(tmp.Pop());
            }
            StringBuilder sb = new();
            foreach (var s in stacks) sb.Append(s.Peek());
            Console.WriteLine("Answer Part 2: " + sb);
        }

        static List<Stack<char>> ParseInput(string[] lines)
        {
            int intialCrates = -1; for (int i = 0; i < lines.Length && intialCrates == -1; i++) { if (lines[i].Length < 2 || lines[i][0] == '[') continue; if (lines[i][1] == '1') intialCrates = i; }
            int crateCount = 0;
            for (int i = 0; i < lines[intialCrates].Length; i++)
            {
                char c = lines[intialCrates][i];
                if (c != '[' && c != ' ' && c != ']') crateCount++;
            }

            List<Stack<char>> crates = new Stack<char>[crateCount].Select(s => new Stack<char>()).ToList();

            for (int i = intialCrates - 1; i >= 0; i--)
            {
                string line = lines[i];

                for (int j = 0, k = 1; j < crateCount; j++, k += 4)
                {
                    if (line[k] == ' ') continue;
                    crates[j].Push(line[k]);
                }
            }

            return crates;
        }

    }
}