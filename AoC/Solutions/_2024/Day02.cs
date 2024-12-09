namespace AoC.Solutions._2024
{
    public class Day02 : AoCDay
    {
        public override void RunPart1()
        {
            string[] data = GetInput();
            var res = 0;
            foreach (var d in data)
            {
                var ar = d.ToIntArray(" ");
                if (SafeArr(ar)) res++;
            }
            Console.WriteLine(res);
        }

        public override void RunPart2()
        {
            string[] data = GetInput();
            var res = 0;
            foreach (var d in data)
            {
                if (Safe(d)) res++;
            }
            Console.WriteLine(res);
        }

        private static bool Safe(string i)
        {
            var ar = i.ToIntArray(" ");
            var res = SafeArr(ar);
            if (res) return true;
            for (int j = 0; j < ar.Length; j++)
            {
                var temparr = ar.ToList();
                temparr.RemoveAt(j);
                if (SafeArr(temparr.ToArray()))
                    return true;
            }
            return false;
        }

        private static bool SafeArr(int[] ar)
        {
            var pref = 0;
            for (int j = 0; j < ar.Length - 1; j++)
            {
                var dif = ar[j] - ar[j + 1];
                if (dif == 0)
                    return false;

                if ((pref < 0 && dif > 0) || (pref > 0 && dif < 0))
                    return false;

                var step = dif < 0 ? dif * -1 : dif;
                if (step > 3)
                    return false;

                pref = dif;
            }
            return true;
        }
    }
}
