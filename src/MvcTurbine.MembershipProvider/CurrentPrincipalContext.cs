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

        private static FormsAuthenticationTicket CreateTheFormsAuthenticationTicket(IPrincipal principal)
        {
            return new DefaultFormsAuthenticationTicketCreator()
                .CreateFormsAuthenticationTicket(principal.Identity.Name, "");
        }

        private static HttpCookie CreateCookieForCurrentUser(IPrincipal principal)
        {
            var encodedTicket = CreateEncryptedTicketForIdentity(principal);
            return new HttpCookie(FormsAuthentication.FormsCookieName, encodedTicket);
        }

        private static string CreateEncryptedTicketForIdentity(IPrincipal principal)
        {
            var ticket = CreateTheFormsAuthenticationTicket(principal);

            return FormsAuthentication.Encrypt(ticket);
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