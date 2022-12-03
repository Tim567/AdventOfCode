using AoC.Interface;

string year = DateTime.Now.Year.ToString();
string day = DateTime.Today.Day.ToString("00");

AoCDay dayClass = Activator.CreateInstance(Type.GetType($"AoC.Solutions._{year}.Day{day}")) as AoCDay;
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


// Console.ReadLine();