namespace URLShortening.Models.RestModels.Request
{
    public class CreateCustomShorteningRestRequestModel
    {
        public string OriginalUrl { get; set; }
        public string CustomShorteningUrl { get; set; }
    }
}
