using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.StringHelper
{
    public static class StringHelper
    {
        public static string GenerateRandomString(int characterLength)
        {

            var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, characterLength)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }


    }
}
