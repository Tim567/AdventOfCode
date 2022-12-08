using Xamarin.Essentials;

namespace AoC.Solutions._2022
{
    public class Day08 : AoCDay
    {
        string[] data;
        public Day08() => Seed();

        void Seed()
        {
            data = GetInput();

        }
        public override void RunPart1()
        {
            var ans = 0;
            for (int y = 0; y < data.Length; y++)
                for (int x = 0; x < data[y].Length; x++)
                {
                    var height = data[y][x];

                    // w
                    var visible = true;
                    for (int j = 0; visible && j < x; j++)
                        if (data[y][j] >= height)
                            visible = false;

                    if (visible)
                    {
                        ans++;
                        continue;
                    }

                    // e
                    visible = true;
                    for (int j = data[y].Length - 1; visible && x < j; j--)
                        if (data[y][j] >= height)
                            visible = false;

                    if (visible)
                    {
                        ans++;
                        continue;
                    }

                    // n
                    visible = true;
                    for (int j = 0; visible && j < y; j++)
                        if (data[j][x] >= height)
                            visible = false;

                    if (visible)
                    {
                        ans++;
                        continue;
                    }

                    // s
                    visible = true;
                    for (int j = data.Length - 1; visible && y < j; j--)
                        if (data[j][x] >= height)
                            visible = false;

                    if (visible)
                    {
                        ans++;
                        continue;
                    }
                }
            Console.WriteLine("Answer Part 1: " + ans);
        }

        public override void RunPart2()
        {
            var ans = 0;
            for (int i = 0; i < data.Length; i++)
                for (int j = 0; j < data[i].Length; j++)
                {
                    int tmp = 0, prod = 1;
                    for (int k = i - 1; k >= 0; k--) { tmp++; if (data[k][j] >= data[i][j]) break; }
                    prod *= tmp; tmp = 0;
                    for (int k = i + 1; k < data.Length; k++) { tmp++; if (data[k][j] >= data[i][j]) break; }
                    prod *= tmp; tmp = 0;
                    for (int k = j - 1; k >= 0; k--) { tmp++; if (data[i][k] >= data[i][j]) break; }
                    prod *= tmp; tmp = 0;
                    for (int k = j + 1; k < data.Length; k++) { tmp++; if (data[i][k] >= data[i][j]) break; }
                    prod *= tmp; tmp = 0;
                    ans = prod > ans ? prod : ans;



                }
            Console.WriteLine("Answer Part 2: " + ans);
        }
    }
}
