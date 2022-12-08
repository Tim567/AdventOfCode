using AoC.Interface;

string year = DateTime.Now.Year.ToString();
string day = DateTime.Today.Day.ToString("00");

//day = 5.ToString("00");

AoCDay dayClass = Activator.CreateInstance(Type.GetType($"AoC.Solutions._{year}.Day{day}")) as AoCDay;
RunDay(dayClass, year, day);
Console.ForegroundColor = ConsoleColor.Magenta;
Console.WriteLine("\nRun previous days? (y/n)");
Console.ForegroundColor = ConsoleColor.White;
if (Console.ReadKey() is ConsoleKeyInfo key && key.Key == ConsoleKey.Y)
{
    Console.SetCursorPosition(0, Console.CursorTop -1);
    Console.WriteLine();
    day = (int.Parse(day) - 1).ToString("00");
    while (day != "00")
    {
        dayClass = Activator.CreateInstance(Type.GetType($"AoC.Solutions._{year}.Day{day}")) as AoCDay;
        RunDay(dayClass, year, day);
        day = (int.Parse(day) - 1).ToString("00");
        // wait 1 second
        Thread.Sleep(300);
    }
}




void RunDay(AoCDay dayClass, string year, string day)
{ 
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("+----------------------------+");
    Console.WriteLine($"| Advent of Code {year} Day {day} |");
    Console.WriteLine("+----------------------------+");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Part 1:");
    Console.ForegroundColor = ConsoleColor.White;
    dayClass.RunPart1();
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("\nPart 2:");
    Console.ForegroundColor = ConsoleColor.White;
    dayClass.RunPart2();
}


