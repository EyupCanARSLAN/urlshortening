using Business.UrlShortening.Interfaces.UrlShortening;
using Business.UrlShortening.Models.UrlShortening.Request;
using Common;
using Microsoft.AspNetCore.Mvc;
using URLShortening.Models.RestModels.Request;

namespace URLShortening.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlJobsController : ControllerBase

    {
        private readonly IUrlShorteningService _urlShorteningService;
        public UrlJobsController(IUrlShorteningService urlShorteningService)
        {
            _urlShorteningService = urlShorteningService;
        }

        /// <summary>
        /// Use this endpoint to create unique url shortening. 
        /// </summary>
        /// <param name="createShorteningRestRequestModel"></param>
        /// <returns></returns>
        [HttpPost("CreateShortening")]
        public ServiceResult<String> CreateShortening(CreateShorteningRestRequestModel createShorteningRestRequestModel)
        {
            CreateUrlShorteningBusinessRequestModel createUrlShorteningBusinessRequestModel = new CreateUrlShorteningBusinessRequestModel();
            createUrlShorteningBusinessRequestModel.OriginalUrl = createShorteningRestRequestModel.OriginalUrl;

            return _urlShorteningService.CreateUrlShortening(createUrlShorteningBusinessRequestModel);
        }

        /// <summary>
        /// Use this endpoint to create unique custom url shortening. 
        /// </summary>
        /// <param name="createShorteningRestRequestModel"></param>
        /// <returns></returns>
        [HttpPost("CreateCustomShortening")]
        public ServiceResult<String> CreateCustomShortening(CreateCustomShorteningRestRequestModel createShorteningRestRequestModel)
        {
            CreateCustomUrlShorteningBusinessRequestModel createUrlShorteningBusinessRequestModel = new CreateCustomUrlShorteningBusinessRequestModel();
            createUrlShorteningBusinessRequestModel.OriginalUrl = createShorteningRestRequestModel.OriginalUrl;
            createUrlShorteningBusinessRequestModel.CustomShorteningUrl = createShorteningRestRequestModel.CustomShorteningUrl;

            return _urlShorteningService.CreateCustomUrlShortening(createUrlShorteningBusinessRequestModel);

        }


        /// <summary>
        /// Find original url with using url shortening
        /// </summary>
        /// <param name="shorenedUrl"></param>
        /// <returns></returns>
        [HttpGet("GetShortening")]
        public ServiceResult<String> FindOriginalUrlByUrlShortening(string shorenedUrl)
        {
            FindOrginalUrlByUrlShorteningBusinessRequestModel findByUrlShorteningBusinessRequestModel = new FindOrginalUrlByUrlShorteningBusinessRequestModel();
            findByUrlShorteningBusinessRequestModel.ShorteningUrl = shorenedUrl;
            return _urlShorteningService.FindOrginalUrlByUrlShortening(findByUrlShorteningBusinessRequestModel);


        }


        /// <summary>
        ///  Url shortening api. 
        ///  This api redirect web page to originalUrl.
        ///  This api can called from prgoram.cs to rerouting.
        ///  You can test this api with your shortening url.
        ///  Exp: http://localhost:5123/pwqazc
        /// </summary>
        /// <param name="urlShortening"></param>
        /// <returns></returns>
        [HttpGet("RedirectShortening")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult RedirectShortening(string urlShortening)
        {
            FindOrginalUrlByUrlShorteningBusinessRequestModel findByUrlShorteningBusinessRequestModel = new FindOrginalUrlByUrlShorteningBusinessRequestModel();
            findByUrlShorteningBusinessRequestModel.ShorteningUrl = urlShortening;
            var result = _urlShorteningService.FindOrginalUrlByUrlShortening(findByUrlShorteningBusinessRequestModel);
            if (result.Status == ServiceResultStatus.Success)
            {
                return new RedirectResult(result.Data);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
