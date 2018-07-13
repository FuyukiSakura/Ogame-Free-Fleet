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
        CookieContainer GetCookies(Uri requestedUri);
        Task<ServerAccount[]> GetAccountsAsync();
    }
}
