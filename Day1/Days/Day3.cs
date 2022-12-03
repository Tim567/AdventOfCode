using Day1.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1.Days
{
    public class Day3 : IAoCDay
    {
        public string[] GetInput()
        {
            return File.ReadAllLines($"Input/03.txt");
        }

        public void Run()
        {
            string[] data = GetInput();
            int total = 0;
            // implement solution
            foreach (var rucksack in data)
            {
                //split rucksack into half
                string left = rucksack.Substring(0, rucksack.Length / 2);
                string right = rucksack.Substring(rucksack.Length / 2);
                //split into inivitual chars
                char[] leftChars = left.ToCharArray();
                char[] rightChars = right.ToCharArray();

                //check what chars are in both
                int count = 0;
                //List<Char> both = new List<char>();
                Dictionary<Char, int> both = new Dictionary<char, int>();
                foreach (var leftChar in leftChars)
                {
                    if (rightChars.Contains(leftChar))
                    {
                        count++;
                        if (both.ContainsKey(leftChar))
                        {
                            both[leftChar]++;
                        }
                        else
                        {
                            both.Add(leftChar, 1);
                        }
                    }
                }
                //Lowercase item types a through z have priorities 1 through 26.
                //Uppercase item types A through Z have priorities 27 through 52.
                //The priority of an item is the sum of the priorities of its characters.
                int priority = 0;
                foreach (var item in both)
                {
                    if (item.Key >= 'a' && item.Key <= 'z')
                    {
                        priority += item.Key - 'a' + 1;
                    }
                    else if (item.Key >= 'A' && item.Key <= 'Z')
                    {
                        priority += item.Key - 'A' + 27;
                    }
                }
                Console.WriteLine($"Count: {count}, Priority: {priority}");
                total += priority;
            }
            Console.WriteLine($"Total: {total}");
        }
    }
}
