using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class UrlShorteningRepository : IUrlShorteningRepository
    {
        public void CreateUrlShortening(UrlShortening urlShortening)
        {
            using (var context = new UrlShorteningDbContext())
            {
                context.Add(urlShortening);
                context.SaveChanges();
            }
        }
        public void DeleteByShorteningUrl(string shorteningUrl)
        {
            throw new NotImplementedException();
        }

        public void DeleteUrlShortening(string shorteningUrl)
        {
            using (var context = new UrlShorteningDbContext())
            {
                var recordInDb= context.UrlShortening.FirstOrDefault(x => x.ShorteningUrl == shorteningUrl && !x.IsDeleted);
                if (recordInDb != null)
                {
                    recordInDb.IsDeleted = true;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception($"The delete progress did not occered. There isnt any record for given url : {shorteningUrl} ");
                }
            }
        }

     

        public UrlShortening? FindByOriginalUrl(string originalUrl)
        {
            using (var context = new UrlShorteningDbContext())
            {
               return context.UrlShortening.FirstOrDefault(x=>x.OriginalUrl== originalUrl && !x.IsDeleted);
            }
        }

        public UrlShortening? FindByShorteningUrl(string shorteningUrl)
        {
            using (var context = new UrlShorteningDbContext())
            {
                return context.UrlShortening.FirstOrDefault(x => x.ShorteningUrl == shorteningUrl && !x.IsDeleted);
            }
        }
    }
}
