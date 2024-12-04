using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookStore
{
    internal static class RegexExtension
    {
        public static bool IsCorrect(string pattern, string text)
        {
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(text);
            if (matches.Count <= 0)
            {
                Console.WriteLine("Введено неверное значение");
                return false;
            }
            return true;
        }
    }
}
