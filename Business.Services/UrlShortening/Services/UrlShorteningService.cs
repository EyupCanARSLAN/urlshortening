
using Business.UrlShortening.Interfaces.UrlShortening;
using Business.UrlShortening.Models.UrlShortening.Request;
using Domain;
using Domain.Repository;
using Common.StringHelper;
using Common;
using Common.Enum;


namespace Business.UrlShortening.Services
{
    public class UrlShorteningService : IUrlShorteningService
    {
        readonly IUrlShorteningRepository _urlShorteningRepository;
        public UrlShorteningService(IUrlShorteningRepository urlShorteningRepository)
        {
            _urlShorteningRepository = urlShorteningRepository;
        }

        /// <summary>
        /// Create a unique url Shortening.
        /// Note: This fucntion prevent to dublicate original url record.
        /// </summary>
        /// <param name="createUrlShorteningBusinessRequestModel"></param>
        /// <returns>Uniqe Url Shortening</returns>
        public ServiceResult<String> CreateUrlShortening(CreateUrlShorteningBusinessRequestModel createUrlShorteningBusinessRequestModel)
        {
            #region Service Validation Rules
            if (!createUrlShorteningBusinessRequestModel.OriginalUrl.IsValidUri())
            {
                return ServiceResult<string>.FailResponse(serviceMessageCode: (int)ServiceMessageCode.InCorrectURLFormat);
            }
            if (_urlShorteningRepository.FindByOriginalUrl(createUrlShorteningBusinessRequestModel.OriginalUrl) != null)
            {
                return ServiceResult<string>.FailResponse(serviceMessageCode: (int)ServiceMessageCode.UrlIsExist);
            }
            #endregion

            #region Generate Unique ShorteningUrl
            var shortUrl = StringHelper.GenerateRandomString(8);
            while (_urlShorteningRepository.FindByShorteningUrl(shortUrl) != null)
            {
                shortUrl = StringHelper.GenerateRandomString(8);
            }
            #endregion

            #region Save To Db
            Domain.Entity.UrlShortening urlShortening = new Domain.Entity.UrlShortening();
            urlShortening.OriginalUrl = createUrlShorteningBusinessRequestModel.OriginalUrl;
            urlShortening.ShorteningUrl = shortUrl;
            urlShortening.CreateTime = DateTime.Now;
            urlShortening.IsDeleted = false;
            _urlShorteningRepository.CreateUrlShortening(urlShortening);
            #endregion
            return ServiceResult<string>.SuccessResponse(shortUrl);
        }

        /// <summary>
        /// Create a custom url Shortening.
        /// Note: The Shortening url maximum length can be 6
        /// Note2: This fucntion prevent to dublicate original url and Shortening url records.
        /// </summary>
        /// <param name="createCustomUrlShorteningBusinessRequestModel"></param>
        /// <returns></returns>
        public ServiceResult<String> CreateCustomUrlShortening(CreateCustomUrlShorteningBusinessRequestModel createCustomUrlShorteningBusinessRequestModel)
        {
            #region Service Validation Rules
            if (!createCustomUrlShorteningBusinessRequestModel.OriginalUrl.IsValidUri())
            {
                return ServiceResult<string>.FailResponse(serviceMessageCode: (int)ServiceMessageCode.InCorrectURLFormat);
            }
            if (String.IsNullOrWhiteSpace(createCustomUrlShorteningBusinessRequestModel.CustomShorteningUrl))
            {
                return ServiceResult<string>.FailResponse(serviceMessageCode: (int)ServiceMessageCode.EmptyShortenURL);
            }
            if (createCustomUrlShorteningBusinessRequestModel.CustomShorteningUrl.Length>6)
            {
                return ServiceResult<string>.FailResponse(serviceMessageCode: (int)ServiceMessageCode.MaxCharacterExceedForShortenURL);
            }
            if (_urlShorteningRepository.FindByOriginalUrl(createCustomUrlShorteningBusinessRequestModel.OriginalUrl) != null)
            {
                return ServiceResult<string>.FailResponse(serviceMessageCode: (int)ServiceMessageCode.UrlIsExist);
            }
            if (_urlShorteningRepository.FindByShorteningUrl(createCustomUrlShorteningBusinessRequestModel.CustomShorteningUrl) != null)
            {
                return ServiceResult<string>.FailResponse(serviceMessageCode: (int)ServiceMessageCode.ShortenUrlIsExist);
            }
            #endregion

            #region Save To Db
            Domain.Entity.UrlShortening urlShortening = new Domain.Entity.UrlShortening();
            urlShortening.OriginalUrl = createCustomUrlShorteningBusinessRequestModel.OriginalUrl;
            urlShortening.ShorteningUrl = createCustomUrlShorteningBusinessRequestModel.CustomShorteningUrl;
            urlShortening.CreateTime = DateTime.Now;
            urlShortening.IsDeleted = false;
            _urlShorteningRepository.CreateUrlShortening(urlShortening);
            #endregion
            return ServiceResult<string>.SuccessResponse(createCustomUrlShorteningBusinessRequestModel.CustomShorteningUrl);
        }


        /// <summary>
        /// Find original url with using Shortening url
        /// </summary>
        /// <param name="findByUrlShorteningBusinessRequestModel"></param>
        /// <returns></returns>
        public ServiceResult<String> FindOrginalUrlByUrlShortening(FindOrginalUrlByUrlShorteningBusinessRequestModel findByUrlShorteningBusinessRequestModel)
        {
            #region Service Validation
            if (String.IsNullOrWhiteSpace(findByUrlShorteningBusinessRequestModel.ShorteningUrl))
            {
                return ServiceResult<String>.FailResponse(serviceMessageCode: (int)ServiceMessageCode.EmptyShortenURL);
            }
            #endregion

            #region Find Url
            var dbResult = _urlShorteningRepository.FindByShorteningUrl(findByUrlShorteningBusinessRequestModel.ShorteningUrl);
            if (dbResult == null)
            {
                return ServiceResult<String>.FailResponse(serviceMessageCode: (int)ServiceMessageCode.ShortenUrlIsExist);
            }
            else
            {
                return ServiceResult<String>.SuccessResponse(dbResult.OriginalUrl);
            }
            #endregion
        }
    }
}
