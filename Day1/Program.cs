using Day1.Days;
using Day1.Interface;

string year = DateTime.Now.Year.ToString();
string day = DateTime.Today.Day.ToString("00");

//IAoCDay day = new Day3();
IAoCDay dayClass = Activator.CreateInstance(Type.GetType($"Day1.Days._{year}.Day{day}")) as IAoCDay;

Console.WriteLine("Part 1:");
dayClass.RunPart1();
Console.WriteLine("\nPart 2:");
dayClass.RunPart2();


// Console.ReadLine();