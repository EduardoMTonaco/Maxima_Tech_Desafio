using System.Net;

namespace Maxima_Tech_Web.Class.Utility
{
    public class HttpContextApi
    {
        private readonly IConfiguration _config;

        public HttpContextApi(IConfiguration config)
        {
            _config = config;
        }

        public string GetApiUrl()
        {
            return _config["ApiSettings:BaseUrl"];
        }
    }
}
