using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using FreeFleet.Extension;
using FreeFleet.Model.Ogame;
using FreeFleet.Resources;
using HtmlAgilityPack;
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

        #region Accounts

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

        #endregion

        #region Game

        public async Task<EventFleet[]> GetEventFleetsAsync(string host)
        {
            // Load Event Fleets page
            var uri = new Uri(string.Format(UriList.OgameEventFleetUrl,
                host));
            var container = GetCookies(uri);
            var response = (HttpWebResponse)await GetResponseAsync(uri.AbsoluteUri, container);
            if (response.StatusCode != HttpStatusCode.OK) return null; // Failed requesting resources

            var doc = new HtmlDocument();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                doc.LoadHtml(await reader.ReadToEndAsync());
            }

            // Parse event fleet page into Event Fleet details
            var fleets = new List<EventFleet>();
            var eventFleetNodes = doc.DocumentNode.SelectNodes("//tr[@class='eventFleet']");
            if (eventFleetNodes == null) return fleets.ToArray(); // No event, return

            foreach (var node in eventFleetNodes)
            {
                // TODO: Null check every node
                var coordOrigin = node.SelectSingleNode("//td[@class='coordsOrigin']/a").InnerHtml.Trim();
                var coordDest = node.SelectSingleNode("//td[@class='destCoords']/a").InnerHtml.Trim();
                var detailsFleet = node.SelectSingleNode("//td[@class='detailsFleet']/span").InnerHtml;
                var missionType = node.Attributes["data-mission-type"].Value;

                fleets.Add(new EventFleet
                {
                    Id = node.Id,
                    CoordsOrigin = coordOrigin,
                    CoordsDest = coordDest,
                    DetailsFleet = Convert.ToInt64(detailsFleet),
                    MissionType = (MissionType)Convert.ToInt16(missionType)
                });
            }

            return fleets.ToArray();
        }

        #endregion

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
