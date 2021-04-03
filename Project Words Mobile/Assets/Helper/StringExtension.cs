using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Helper
{
    public static class StringExtension
    {

        public static IEnumerable<String> ToListString(this string word)
        {
            if (word == null)
            {
                throw new ArgumentNullException("The word is null cannot be converted");
            }
            foreach (var c in word)
            {
                yield return c.ToString();
            }
        }
    }
}
