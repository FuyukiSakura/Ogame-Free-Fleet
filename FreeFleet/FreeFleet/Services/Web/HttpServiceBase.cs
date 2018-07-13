using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using FreeFleet.Extension;
using FreeFleet.Model.Ogame;
using FreeFleet.Resources;
using Xamarin.Forms;

namespace FreeFleet.Services.Web
{
    public abstract class HttpServiceBase
    {
        /// <summary>
        /// Send HTML request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookieContainer"></param>
        /// <returns></returns>
        public async Task<WebResponse> GetResponseAsync(string url, CookieContainer cookieContainer)
        {
            var request = GenerateBrowserSimulatedWebRequest(url);
            request.CookieContainer = cookieContainer;
            return await request.GetResponseAsync();
        }
       
        public async Task<ServerAccount[]> GetAccountsAsync()
        {
            var uri = new Uri(UriList.OgameAccountList);
            var container = GetCookies(uri);
            var response = (HttpWebResponse)await GetResponseAsync(uri.AbsoluteUri, container);
            if (response.StatusCode != HttpStatusCode.OK) return null; // Failed requesting resources

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return (await reader.ReadToEndAsync()).JsonDeserialize<ServerAccount[]>();
            }
        }

        public async Task<LobbyLogin> LoginAccountAsync(ServerAccount account)
        {
            var uri = new Uri(string.Format(UriList.OgameLoginUrl,
                account.Id,
                account.Server.Language, 
                account.Server.Number));
            var container = GetCookies(uri);
            var response = (HttpWebResponse)await GetResponseAsync(uri.AbsoluteUri, container);
            if (response.StatusCode != HttpStatusCode.OK) return null; // Failed requesting resources

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return (await reader.ReadToEndAsync()).JsonDeserialize<LobbyLogin>();
            }
        }

        #region Internal functions

        /// <summary>
        /// Generate a web request that looks like a browser request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static HttpWebRequest GenerateBrowserSimulatedWebRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers[HttpRequestHeader.UserAgent] = BrowserInfo.UserAgent;
            return request;
        }

        #endregion

        #region Abstract functions

        public abstract CookieContainer GetCookies(Uri requestedUri);

        #endregion

    }
}
