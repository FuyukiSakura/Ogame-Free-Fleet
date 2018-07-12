using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FreeFleet.Services.Web
{
    public interface IHttpService
    {
        CookieContainer GetCookies(Uri requestedUri);
    }
}
