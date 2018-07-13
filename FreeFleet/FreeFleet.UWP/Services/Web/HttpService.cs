using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http.Filters;
using FreeFleet.Services.Web;
using FreeFleet.UWP.Services.Web;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpService))]
namespace FreeFleet.UWP.Services.Web
{
    public class HttpService : HttpServiceBase, IHttpService
    {
        public override CookieContainer GetCookies(Uri requestedUri)
        {
            var httpBaseProtocolFilter = new HttpBaseProtocolFilter();
            var cookieManager = httpBaseProtocolFilter.CookieManager;
            var cookieCollection = cookieManager.GetCookies(requestedUri);
            var cookieContainer = new CookieContainer();
            foreach (var cookie in cookieCollection)
            {
                // Convert between the System.Net.Cookie to a System.Web.HttpCookie...
                cookieContainer.Add(new Cookie
                {
                    Domain = requestedUri.Host,
                    Expires = ((DateTimeOffset)cookie.Expires).DateTime,
                    Name = cookie.Name,
                    Path = cookie.Path,
                    Secure = cookie.Secure,
                    Value = cookie.Value
                });
            }

            return cookieContainer;
        }
    }
}
