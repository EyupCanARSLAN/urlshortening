using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.StringHelper
{
    public static class StringExtension
    {

        public static bool IsValidUri (this string inputString)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(inputString, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }
            
    }
}
