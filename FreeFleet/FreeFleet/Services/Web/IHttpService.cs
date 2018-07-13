using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FreeFleet.Model.Ogame;

namespace FreeFleet.Services.Web
{
    public interface IHttpService
    {
        /// <summary>
        /// Get cookie from the requested URL from the device
        /// </summary>
        /// <param name="requestedUri"></param>
        /// <returns></returns>
        CookieContainer GetCookies(Uri requestedUri);

        #region Ogame functions

        /// <summary>
        /// Get accounts from Ogame Lobby
        /// </summary>
        /// <returns></returns>
        Task<ServerAccount[]> GetAccountsAsync();

        /// <summary>
        /// Login to the account through Lobby API
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<LobbyLogin> LoginAccountAsync(ServerAccount account);

        #endregion
    }
}
