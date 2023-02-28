using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enum
{
    public enum ServiceMessageCode : int
    {
      InCorrectURLFormat=1000,
      UrlIsExist = 1001,
      ShortenUrlIsExist = 1002,
      EmptyShortenURL =1003,
      MaxCharacterExceedForShortenURL =1004
       
    }
}
