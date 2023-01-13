namespace AoC.Solutions._2022
{
    public class Day09 : AoCDay
    {
        string[] data;
        public Day09() => Seed();

        void Seed()
        {
            data = GetInput();

        }
        
        public override void RunPart1()
        {
            var ans = Solve(2);
            Console.WriteLine("Answer Part 1: " + ans);
        }

        public override void RunPart2()
        {
            var ans = Solve(10);

            Console.WriteLine("Answer Part 2: " + ans);
        }

        private int Solve(int ropeLength)
        {
            var rope = new (int X, int Y)[ropeLength];
            Array.Fill(rope, (0, 0));
            var tail = ropeLength - 1;
            var visited = new HashSet<(int X, int Y)> { rope[tail] };

            foreach (var line in data)
            {
                var values = line.Split(' ');
                var direction = values[0];
                var steps = int.Parse(values[1]);

                for (var i = 0; i < steps; i++)
                {
                    MoveHead(ref rope[0], direction);
                    UpdateKnots(rope);
                    visited.Add(rope[tail]);
                }
            }

            return visited.Count;
        }

        private static void MoveHead(ref (int X, int Y) head, string direction)
        {
            switch (direction)
            {
                case "R":
                    head.X++;
                    break;
                case "L":
                    head.X--;
                    break;
                case "U":
                    head.Y++;
                    break;
                case "D":
                    head.Y--;
                    break;
            }
        }

        private static void UpdateKnots((int X, int Y)[] rope)
        {
            for (var j = 1; j < rope.Length; j++)
            {
                if (AreAdjacent(rope[j - 1], rope[j]))
                {
                    continue;
                }

                MoveKnotTowardsPrevious(ref rope[j], ref rope[j - 1]);
            }
        }

        private static bool AreAdjacent((int X, int Y) first, (int X, int Y) second)
        {
            var adjacentCells = new List<(int X, int Y)>
        {
            (first.X - 1, first.Y - 1),
            (first.X    , first.Y - 1),
            (first.X + 1, first.Y - 1),
            (first.X - 1, first.Y    ),
            (first.X    , first.Y    ),
            (first.X + 1, first.Y    ),
            (first.X - 1, first.Y + 1),
            (first.X    , first.Y + 1),
            (first.X + 1, first.Y + 1),
        };

            return adjacentCells.Contains(second);
        }

        private static void MoveKnotTowardsPrevious(ref (int X, int Y) current, ref (int X, int Y) previous)
        {
            if (previous.Y == current.Y)
            {
                current.X += previous.X > current.X ? 1 : -1;
            }
            else if (previous.X == current.X)
            {
                current.Y += previous.Y > current.Y ? 1 : -1;
            }
            else
            {
                var dX = previous.X - current.X;
                var dY = previous.Y - current.Y;

                current.X += dX > 0 ? 1 : -1;
                current.Y += dY > 0 ? 1 : -1;
            }
        }
    }
}
