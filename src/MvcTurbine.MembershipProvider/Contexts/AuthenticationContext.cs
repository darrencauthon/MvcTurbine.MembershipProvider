using System.Security.Principal;
using System.Web.Security;
using MvcTurbine.MembershipProvider.PrincipalHelpers;

namespace MvcTurbine.MembershipProvider.Contexts
{
    public interface IAuthenticationContext
    {
        void Authenticate(IPrincipal principal);
    }

    public class AuthenticationContext : IAuthenticationContext
    {
        private readonly IPrincipalCreator principalCreator;
        private readonly IPrincipalContext principalContext;

        public AuthenticationContext(IPrincipalCreator principalCreator,
                                     IPrincipalContext principalContext)
        {
            this.principalCreator = principalCreator;
            this.principalContext = principalContext;
        }

        public void Authenticate(IPrincipal principal)
        {
            if (ThisIsAValidPrincipal(principal))
                UseThePrincipleFromTheTicket(principal);
            else
                UseAnUnauthenticatedPrinciple();
        }

        private void UseAnUnauthenticatedPrinciple()
        {
            principalContext.SetPricipal(principalCreator.CreateUnauthenticatedPrincipal());
        }

        private void UseThePrincipleFromTheTicket(IPrincipal principal)
        {
            var ticket = GetTheTicketFromThePrincipal(principal);
            var principalFromTicket = principalCreator.CreatePrincipalFromTicket(ticket);
            principalContext.SetPricipal(principalFromTicket);
        }

        private static FormsAuthenticationTicket GetTheTicketFromThePrincipal(IPrincipal principal)
        {
            return ((FormsIdentity) principal.Identity).Ticket;
        }

        private static bool ThisIsAValidPrincipal(IPrincipal principal)
        {
            return principal != null && principal.Identity.IsAuthenticated &&
                   principal.Identity.GetType() == typeof (FormsIdentity);
        }
    }
}