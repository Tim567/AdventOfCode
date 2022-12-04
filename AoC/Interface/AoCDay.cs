using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AoC.Interface
{
    public abstract class AoCDay
    {
        public string[] GetInput()
        {
            var res = File.ReadAllLines($"Input/{GetType().Namespace[^5..]}/{GetType().Name.Replace("Day", "")}.txt");
            if (res.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No input found!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Use clipboard to set input? (y/n)");
                Console.ForegroundColor = ConsoleColor.White;
                if (Console.ReadKey() is ConsoleKeyInfo key && key.Key == ConsoleKey.Y)
                {
                    var input = GetText();
                    File.WriteAllLines($"Input/{GetType().Namespace[^5..]}/{GetType().Name.Replace("Day", "")}.txt", input.Split(Environment.NewLine));
                    File.WriteAllLines($"../../../Input/{GetType().Namespace[^5..]}/{GetType().Name.Replace("Day", "")}.txt", input.Split(Environment.NewLine));
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Input saved to file: " + $"Input/{GetType().Namespace[^5..]}/{GetType().Name.Replace("Day", "")}.txt");
                    Console.ForegroundColor = ConsoleColor.White;
                    res = input.Split(Environment.NewLine);
                    
                }
            }

            return res;
        }
        public abstract void RunPart1();
        public abstract void RunPart2();

        private static string GetText()
        {
            var powershell = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    RedirectStandardOutput = true,
                    FileName = "powershell",
                    Arguments = "-command \"Get-Clipboard\""
                }
            };

            powershell.Start();
            string text = powershell.StandardOutput.ReadToEnd();
            powershell.StandardOutput.Close();
            powershell.WaitForExit();
            return text.TrimEnd();
        }
    }
}
