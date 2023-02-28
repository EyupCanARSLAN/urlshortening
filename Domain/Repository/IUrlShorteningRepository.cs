using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IUrlShorteningRepository
    {
        public UrlShortening? FindByShorteningUrl(string shorteningUrl);
        public UrlShortening? FindByOriginalUrl(string originalUrl);
        public void CreateUrlShortening(UrlShortening urlShortening);
        public void DeleteByShorteningUrl(string shorteningUrl);
    }


}
