using DeployCmsData.Umbraco7.Interfaces;
using System.Web;

namespace DeployCmsData.Umbraco7.Services
{
    internal class Server : IHttpServerUtility
    {
        private HttpServerUtility _httpServerUtility;

        public Server()
        {
            _httpServerUtility = HttpContext.Current.Server;
        }

        public string MapPath(string path)
        {
            return _httpServerUtility.MapPath(path);
        }

        public string ReadAllText(string path)
        {
            var mappedPath = MapPath(path);
            if (System.IO.File.Exists(mappedPath))
            {
                return System.IO.File.ReadAllText(mappedPath);
            }

            return string.Empty;
        }
    }
}
