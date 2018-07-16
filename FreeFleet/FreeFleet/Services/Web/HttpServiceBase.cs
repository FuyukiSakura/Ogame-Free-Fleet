using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using FreeFleet.Core;
using FreeFleet.Extension;
using FreeFleet.Model.Ogame;
using FreeFleet.Resources;
using FreeFleet.Resources.Localization.General;
using HtmlAgilityPack;
using Xamarin.Forms;
using DebugMessage = FreeFleet.Resources.Localization.General.DebugMessageResources;

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
            if (response.StatusCode != HttpStatusCode.OK)
            {
                // Failed requesting resources
                Logger.Log(string.Format(DebugMessage.EventFleetRefreshFail, 
                    DebugMessage.EventFleetRefreshRejected, 
                    ""));
                return null;
            }

            // Check if logged out
            if (response.ResponseUri.Host == UriList.OgameLobbyHost){
                // Logged out return
                Logger.Log(string.Format(DebugMessage.EventFleetRefreshFail,
                    DebugMessage.EventFleetRefreshLoggedOut,
                    ""));
                return null;
            }

            var doc = new HtmlDocument();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                doc.LoadHtml(await reader.ReadToEndAsync());
            }

            // Parse event fleet page into Event Fleet details
            var fleets = new List<EventFleet>();
            var eventFleetNodes = doc.DocumentNode.SelectNodes("//tr[@class='eventFleet']");
            if (eventFleetNodes == null) {
                Logger.Log(DebugMessage.EventFleetRefreshEmptyFleet);
                return fleets.ToArray(); // No event, return
            }

            fleets.AddRange(from node in eventFleetNodes
                let coordOrigin = node.SelectSingleNode("./td[@class='coordsOrigin']/a")?.InnerHtml.Trim() ?? SharedResources.NodeNotFound
                let coordDest = node.SelectSingleNode("./td[@class='destCoords']/a")?.InnerHtml.Trim() ?? SharedResources.NodeNotFound
                let detailsFleet = node.SelectSingleNode("./td[@class='detailsFleet']/span")?.InnerHtml ?? "0"
                let missionType = node.Attributes["data-mission-type"]?.Value ?? "1"
                select new EventFleet
                {
                    Id = node.Id,
                    CoordsOrigin = coordOrigin,
                    CoordsDest = coordDest,
                    DetailsFleet = Convert.ToInt64(detailsFleet),
                    MissionType = (MissionType) Convert.ToInt16(missionType)
                });

            Logger.Log(DebugMessage.EventFleetRefreshSuccess);
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
