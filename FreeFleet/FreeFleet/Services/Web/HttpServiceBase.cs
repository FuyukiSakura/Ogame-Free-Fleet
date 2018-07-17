using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        /// <param name="uri"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> HttpSendRequestAsync(Uri uri)
        {
            var handler = new HttpClientHandler
            {
                CookieContainer = GetCookies(uri)
            };
            var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            request.Headers.Add("User-Agent", BrowserInfo.UserAgent);
            var client = new HttpClient(handler);
            return await client.SendAsync(request);
        }

        #region Accounts

        public async Task<ServerAccount[]> GetAccountsAsync()
        {
            var uri = new Uri(UriList.OgameAccountList);
            var response = await HttpSendRequestAsync(uri);
            return !response.IsSuccessStatusCode ? null : (await response.Content.ReadAsStringAsync()).JsonDeserialize<ServerAccount[]>();
        }

        public async Task<LobbyLogin> LoginAccountAsync(ServerAccount account)
        {
            var uri = new Uri(string.Format(UriList.OgameLoginUrl,
                account.Id,
                account.Server.Language, 
                account.Server.Number));
            var response = await HttpSendRequestAsync(uri);
            if (!response.IsSuccessStatusCode) return null; // Failed requesting resources
            return (await response.Content.ReadAsStringAsync()).JsonDeserialize<LobbyLogin>();
        }

        #endregion

        #region Game

        public async Task<EventFleet[]> GetEventFleetsAsync(string host)
        {
            // Load Event Fleets page
            var uri = new Uri(string.Format(UriList.OgameEventFleetUrl,
                host));
            var response = await HttpSendRequestAsync(uri);
            if (!response.IsSuccessStatusCode)
            {
                // Failed requesting resources
                Logger.Log(string.Format(DebugMessage.EventFleetRefreshFail, 
                    DebugMessage.EventFleetRefreshRejected, 
                    ""));
                return null;
            }

            // Check if logged out
            if (response.RequestMessage.RequestUri.Host == UriList.OgameLobbyHost){
                // Logged out return
                Logger.Log(string.Format(DebugMessage.EventFleetRefreshFail,
                    DebugMessage.EventFleetRefreshLoggedOut,
                    ""));
                return null;
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(await response.Content.ReadAsStringAsync());

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

        #region Abstract functions

        public abstract CookieContainer GetCookies(Uri requestedUri);

        #endregion

    }
}
