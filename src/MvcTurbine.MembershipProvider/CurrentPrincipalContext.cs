using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace MvcTurbine.MembershipProvider
{
    public interface ICurrentPrincipalContext
    {
        void Set(IPrincipal principal);
    }

    public class CurrentPrincipalContext : ICurrentPrincipalContext
    {
        public void Set(IPrincipal principal)
        {
            HttpContext.Current.User = principal;

            var cookieForCurrentIdentity = CreateCookieForCurrentUser(principal);

            var cookies = GetCookies();
            cookies.Add(cookieForCurrentIdentity);
        }

        private static string CreateEncryptedTicketForIdentity(IPrincipal principal)
        {
            var ticket = new FormsAuthenticationTicket(1, principal.Identity.Name,
                                                       DateTime.Now,
                                                       DateTime.Now.AddMinutes(2880),
                                                       true,
                                                       "",
                                                       FormsAuthentication.FormsCookiePath);

            return FormsAuthentication.Encrypt(ticket);
        }

        private static HttpCookie CreateCookieForCurrentUser(IPrincipal principal)
        {
            var encodedTicket = CreateEncryptedTicketForIdentity(principal);
            return new HttpCookie(FormsAuthentication.FormsCookieName, encodedTicket);
        }

        private static HttpCookieCollection GetCookies()
        {
            return GetHttpContext().Cookies;
        }

        private static HttpResponse GetHttpContext()
        {
            return HttpContext.Current.Response;
        }
    }
}