using Business.UrlShortening.Models.UrlShortening.Request;
using Common;

namespace Business.UrlShortening.Interfaces.UrlShortening
{
    public interface IUrlShorteningService
    {
        public ServiceResult<String> CreateUrlShortening(CreateUrlShorteningBusinessRequestModel createUrlShorteningBusinessRequestModel);
        public ServiceResult<String> CreateCustomUrlShortening(CreateCustomUrlShorteningBusinessRequestModel createCustomUrlShorteningBusinessRequestModel);

        public ServiceResult<string> FindOrginalUrlByUrlShortening(FindOrginalUrlByUrlShorteningBusinessRequestModel findByUrlShorteningBusinessRequestModel);
    }
}
