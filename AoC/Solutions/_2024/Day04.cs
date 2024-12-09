using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AoC.Solutions._2024
{
    public class Day04 : AoCDay
    {
        public override void RunPart1()
        {
            string[] data = GetInput();
            var res = 0;

            int R = data.Length;
            int C = data[0].Length;

            for (int r = 0; r < data.Length; r++)
            {
                for (int c = 0; c < data[r].Length; c++)
                {
                    if (c + 3 < C && data[r][c] == 'X' && data[r][c + 1] == 'M' && data[r][c + 2] == 'A' && data[r][c + 3] == 'S')
                        res += 1;
                    if (r + 3 < R && data[r][c] == 'X' && data[r + 1][c] == 'M' && data[r + 2][c] == 'A' && data[r + 3][c] == 'S')
                        res += 1;
                    if (r + 3 < R && c + 3 < C && data[r][c] == 'X' && data[r + 1][c + 1] == 'M' && data[r + 2][c + 2] == 'A' && data[r + 3][c + 3] == 'S')
                        res += 1;
                    if (c + 3 < C && data[r][c] == 'S' && data[r][c + 1] == 'A' && data[r][c + 2] == 'M' && data[r][c + 3] == 'X')
                        res += 1;
                    if (r + 3 < R && data[r][c] == 'S' && data[r + 1][c] == 'A' && data[r + 2][c] == 'M' && data[r + 3][c] == 'X')
                        res += 1;
                    if (r + 3 < R && c + 3 < C && data[r][c] == 'S' && data[r + 1][c + 1] == 'A' && data[r + 2][c + 2] == 'M' && data[r + 3][c + 3] == 'X')
                        res += 1;
                    if (r - 3 >= 0 && c + 3 < C && data[r][c] == 'S' && data[r - 1][c + 1] == 'A' && data[r - 2][c + 2] == 'M' && data[r - 3][c + 3] == 'X')
                        res += 1;
                    if (r - 3 >= 0 && c + 3 < C && data[r][c] == 'X' && data[r - 1][c + 1] == 'M' && data[r - 2][c + 2] == 'A' && data[r - 3][c + 3] == 'S')
                        res += 1;
                }
            }

            Console.WriteLine(res);
        }

        public override void RunPart2()
        {
            string[] data = GetInput();
            var res = 0;

            int R = data.Length;
            int C = data[0].Length;

            for (int r = 0; r < data.Length; r++)
            {
                for (int c = 0; c < data[r].Length; c++)
                {
                    if (r + 2 < R && c + 2 < C && data[r][c] == 'M' && data[r + 1][c + 1] == 'A' && data[r + 2][c + 2] == 'S' && data[r + 2][c] == 'M' && data[r][c + 2] == 'S')
                        res += 1;
                    if (r + 2 < R && c + 2 < C && data[r][c] == 'M' && data[r + 1][c + 1] == 'A' && data[r + 2][c + 2] == 'S' && data[r + 2][c] == 'S' && data[r][c + 2] == 'M')
                        res += 1;
                    if (r + 2 < R && c + 2 < C && data[r][c] == 'S' && data[r + 1][c + 1] == 'A' && data[r + 2][c + 2] == 'M' && data[r + 2][c] == 'M' && data[r][c + 2] == 'S')
                        res += 1;
                    if (r + 2 < R && c + 2 < C && data[r][c] == 'S' && data[r + 1][c + 1] == 'A' && data[r + 2][c + 2] == 'M' && data[r + 2][c] == 'S' && data[r][c + 2] == 'M')
                        res += 1;
                }
            }

            Console.WriteLine(res);
        }

    }
}
