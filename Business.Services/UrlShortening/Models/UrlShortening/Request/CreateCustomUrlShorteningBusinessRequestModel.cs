using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.UrlShortening.Models.UrlShortening.Request
{
    public class CreateCustomUrlShorteningBusinessRequestModel
    {
        public string OriginalUrl { get; set; }
        public string CustomShorteningUrl { get; set; }
    }
}
