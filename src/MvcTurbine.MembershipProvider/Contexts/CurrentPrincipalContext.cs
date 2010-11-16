using System;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using MvcTurbine.MembershipProvider.PrincipalHelpers;

namespace MvcTurbine.MembershipProvider.Contexts
{
    public interface ICurrentPrincipalContext
    {
        void Set(IPrincipal principal, Type type);
    }

    public class CurrentPrincipalContext : ICurrentPrincipalContext
    {
        private readonly IFormsAuthenticationTicketCreator formsAuthenticationTicketCreator;

        public CurrentPrincipalContext(IFormsAuthenticationTicketCreator formsAuthenticationTicketCreator)
        {
            this.formsAuthenticationTicketCreator = formsAuthenticationTicketCreator;
        }

        public void Set(IPrincipal principal, Type type)
        {
            HttpContext.Current.User = principal;

            var cookieForCurrentIdentity = CreateCookieForCurrentUser(principal, type);

            var cookies = GetCookies();
            cookies.Add(cookieForCurrentIdentity);
        }

        private FormsAuthenticationTicket CreateTheFormsAuthenticationTicket(IPrincipal principal, Type type)
        {
            var ticket = formsAuthenticationTicketCreator
                .CreateFormsAuthenticationTicket(principal, type);

            return ticket;
        }

        private HttpCookie CreateCookieForCurrentUser(IPrincipal principal, Type type)
        {
            var encodedTicket = CreateEncryptedTicketForIdentity(principal, type);
            return new HttpCookie(FormsAuthentication.FormsCookieName, encodedTicket);
        }

        private string CreateEncryptedTicketForIdentity(IPrincipal principal, Type type)
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