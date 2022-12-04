namespace AoC.Solutions._2022
{
    public class Day02 : AoCDay
    {

        public override void RunPart1() { }

        public override void RunPart2()
        {
            // Load input.txt as data 
            string[] data = GetInput();
            //rock = A for player 1 ,X for player 2 and 1 point
            //paper = B for player 1, Y for player 2 and 2 points
            //scissors = C for player 1, Z for player 2 and 3 points 

            int player1 = 0;
            int player2 = 0;

            int i = 0;
            void Draw()
            {
                player1 += 3;
                player2 += 3;
                i++;
            }

            void SetNewData(string[] items)
            {

                if (items[0] == "A")
                {
                    if (items[1] == "Z") items[1] = "Y";
                    else if (items[1] == "Y") items[1] = "X";
                    else if (items[1] == "X") items[1] = "Z";
                }
                else if (items[0] == "B")
                {
                    if (items[1] == "Z") items[1] = "Z";
                    else if (items[1] == "Y") items[1] = "Y";
                    else if (items[1] == "X") items[1] = "X";
                }
                else if (items[0] == "C")
                {
                    if (items[1] == "Z") items[1] = "X";
                    else if (items[1] == "Y") items[1] = "Z";
                    else if (items[1] == "X") items[1] = "Y";
                }
            }

            foreach (var item in data)
            {

                var items = item.Split(' ');
                SetNewData(items);
                if (items[0] == "A")
                {
                    if (items[1] == "X") Draw();
                    else if (items[1] == "Y") player2 += 6;
                    else if (items[1] == "Z") player1 += 6;
                }
                else if (items[0] == "B")
                {
                    if (items[1] == "X") player1 += 6;
                    else if (items[1] == "Y") Draw();
                    else if (items[1] == "Z") player2 += 6;
                }
                else if (items[0] == "C")
                {
                    if (items[1] == "X") player2 += 6;
                    else if (items[1] == "Y") player1 += 6;
                    else if (items[1] == "Z") Draw();
                }

                if (items[0] == "A") player1 += 1;
                else if (items[0] == "B") player1 += 2;
                else if (items[0] == "C") player1 += 3;

                if (items[1] == "X") player2 += 1;
                else if (items[1] == "Y") player2 += 2;
                else if (items[1] == "Z") player2 += 3;
            }

            Console.WriteLine("Points player 1: " + player1);
            Console.WriteLine("Points player 2: " + player2);

            Console.WriteLine("Total " + (player1 + player2));

            // part 2

        }
    }
}
