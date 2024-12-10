using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AoC.Utils
{
    public static class StringUtils
    {
        public static string Reverse(this string str)
        {
            char[] arr = str.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public static string[] SplitByNewline(this string str, bool shouldTrim = false) => str
            .Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.None)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => shouldTrim ? s.Trim() : s)
            .ToArray();

        public static string[] SplitByParagraph(this string str, bool shouldTrim = false) => str
            .Split(new[] { "\r\r", "\n\n", "\r\n\r\n" }, StringSplitOptions.None)
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => shouldTrim ? s.Trim() : s)
            .ToArray();

        public static string[] SplitAtIndex(this string input, int index)
            => new string[] { input.Substring(0, index), input.Substring(index) };

        public static int[] ToIntArray(this string str, string delimiter = "")
        {
            if (delimiter == "")
            {
                var result = new List<int>();
                foreach (char c in str) if (int.TryParse(c.ToString(), out int n)) result.Add(n);
                return result.ToArray();
            }
            else
            {
                return str
                    .Split(delimiter)
                    .Where(n => int.TryParse(n, out int v))
                    .Select(n => Convert.ToInt32(n))
                    .ToArray();
            }
        }

        public static long[] ToLongArray(this string str, string delimiter = "")
        {
            if (delimiter == "")
            {
                var result = new List<long>();
                foreach (char c in str) if (long.TryParse(c.ToString(), out long n)) result.Add(n);
                return result.ToArray();
            }
            else
            {
                return str
                    .Split(delimiter)
                    .Where(n => long.TryParse(n, out long v))
                    .Select(n => Convert.ToInt64(n))
                    .ToArray();
            }
        }

        public static bool OnlyDigits(this string str) => str.All(char.IsDigit);

        public static string ToOneLongSting(this string[] input)
        {
            var res = new StringBuilder();
            foreach (var s in input)
            {
                res.Append(s);
            }
            return res.ToString();
        }

        public static List<int> GetAllIndices(this string input, string search)
        {
            List<int> indices = new List<int>();
            int index = input.IndexOf(search);

            while (index != -1)
            {
                indices.Add(index);
                index = input.IndexOf(search, index + 1); // Search for the next occurrence
            }

            return indices;
        }

        public static string[] DataAboveLine(this string[] data, string splitLine)
        {
            List<string> returnData = new();
            foreach (var s in data)
            {
                if (s.Equals(splitLine)) return returnData.ToArray();
                returnData.Add(s);
            }
            return returnData.ToArray();
        }

        public static string[] DataBelowLine(this string[] data, string splitLine)
        {
            List<string> returnData = new();
            bool fill = false;
            foreach (var s in data)
            {
                if (fill) returnData.Add(s);
                if (s.Equals(splitLine)) fill = true;
            }
            return returnData.ToArray();
        }

        public static int ToInt(this string input)
        {
            return int.Parse(input);
        }

        public static long ToLong(this string input) 
        {
            return long.Parse(input); 
        }
    }
}