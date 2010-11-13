using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;

namespace MvcTurbine.MembershipProvider
{
    public interface ICurrentPrincipalContext
    {
        void Set(IPrincipal principal, Type type);
    }

    public class CurrentPrincipalContext : ICurrentPrincipalContext
    {
        public void Set(IPrincipal principal, Type type)
        {
            HttpContext.Current.User = principal;

            var cookieForCurrentIdentity = CreateCookieForCurrentUser(principal, type);

            var cookies = GetCookies();
            cookies.Add(cookieForCurrentIdentity);
        }

        private static FormsAuthenticationTicket CreateTheFormsAuthenticationTicket(IPrincipal principal, Type type)
        {
            var ticket = new DefaultFormsAuthenticationTicketCreator()
                .CreateFormsAuthenticationTicket(principal, type);

            return ticket;
        }

        private static HttpCookie CreateCookieForCurrentUser(IPrincipal principal, Type type)
        {
            var encodedTicket = CreateEncryptedTicketForIdentity(principal, type);
            return new HttpCookie(FormsAuthentication.FormsCookieName, encodedTicket);
        }

        private static string CreateEncryptedTicketForIdentity(IPrincipal principal, Type type)
        {
            var ticket = CreateTheFormsAuthenticationTicket(principal, type);

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