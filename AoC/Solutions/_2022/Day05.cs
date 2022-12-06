using System.Net;

namespace AoC.Solutions._2022
{
    public class Day05 : AoCDay
    {
        string[] data;
        List<Stack<char>> stacks;
        List<(int cnt, int src, int dest)> instructions;
        public Day05() => Seed();

        int defCurY = 0;

        private void Seed()
        {
            data = GetInput();
            stacks = ParseInput(data);
            string[] movements = data.Skip(10).ToArray();
            instructions = new();
            foreach (var move in movements)
            {
                var tokens = move.Split(" ");
                instructions.Add((int.Parse(tokens[1]), int.Parse(tokens[3]), int.Parse(tokens[^1])));
            }
        }

        public override void RunPart1()
        {
            Seed();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Do you want to visualize the process? (y/n)");
            Console.ForegroundColor = ConsoleColor.White;
            bool visualize = Console.ReadKey().Key == ConsoleKey.Y;
            defCurY = Console.CursorTop;
            foreach (var ins in instructions)
            {
                for (int i = 0; i < ins.cnt; i++)
                {
                    stacks[ins.dest - 1].Push(stacks[ins.src - 1].Pop());
                    if (visualize) VisualizeCurrentStacks();
                }

            };
            StringBuilder sb = new();
            foreach (var s in stacks) sb.Append(s.Peek());
            //VisualizeCurrentStacks();
            Console.WriteLine("Answer Part 1: " + sb);
        }

        public override void RunPart2()
        {
            Seed();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Do you want to visualize the process? (y/n)");
            Console.ForegroundColor = ConsoleColor.White;
            bool visualize = Console.ReadKey().Key == ConsoleKey.Y;
            defCurY = Console.CursorTop;
            foreach (var ins in instructions)
            {
                Stack<char> tmp = new Stack<char>();
                for (int i = 0; i < ins.cnt; i++) tmp.Push(stacks[ins.src - 1].Pop());
                while (tmp.Count > 0) stacks[ins.dest - 1].Push(tmp.Pop());
                if (visualize) VisualizeCurrentStacks();
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

        void VisualizeCurrentStacks()
        {
            // Visualize current stacks like:
            //  [N] [Q] [N]
            //  [R] [F] [Q] [G] [M]
            //  [J] [Z] [T] [R] [H] [J]
            //  [T] [H] [G] [R] [B] [N] [T]
            //  [Z] [J] [J] [G] [F] [Z] [S] [M]
            //  [B] [N] [N] [N] [Q] [W] [L] [Q] [S]
            //  [D] [S] [R] [V] [T] [C] [C] [N] [G]
            //  [F] [R] [C] [F] [L] [Q] [F] [D] [P]
            //  1   2   3   4   5   6   7   8   9

            Thread.Sleep(1);
            
            var stacksCopy = new List<Stack<char>>();
            foreach (var s in stacks) stacksCopy.Add(new Stack<char>(s.Reverse()));
            Console.SetCursorPosition(0, defCurY);

            //int highestStackCount = stacksCopy.Select(s => s.Count).Max();
            int highestStackCount = 35;
            for (int i = 0; i < highestStackCount; i++)
            {
                StringBuilder sb = new();
                for (int j = 0; j < stacksCopy.Count; j++)
                {
                    if (highestStackCount - i <= stacksCopy[j].Count) sb.Append($"[{stacksCopy[j].Pop()}] ");
                    else sb.Append("    ");
                }
                Console.WriteLine(sb);
            }
            Console.WriteLine(" 1   2   3   4   5   6   7   8   9");

        }

    }
}