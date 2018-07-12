using System.Net;
using System.Threading.Tasks;

namespace FreeFleet.Services.Web
{
    public class HttpServiceBase
    {
        /// <summary>
        /// Send HTML request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        public async Task<WebResponse> GetResponseAsync(string url, CookieContainer cookieContainer)
        {
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.CookieContainer = cookieContainer;
            return await request.GetResponseAsync();
        }
    }
}
