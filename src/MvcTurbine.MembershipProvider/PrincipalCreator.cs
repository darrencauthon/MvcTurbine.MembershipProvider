using System;
using System.Security.Principal;
using System.Web.Security;

namespace MvcTurbine.MembershipProvider
{
    public class PrincipalCreator : IPrincipalCreator
    {
        private readonly IUnauthorizedPrincipalCreator unauthorizedPrincipalCreator;

        public PrincipalCreator(IUnauthorizedPrincipalCreator unauthorizedPrincipalCreator)
        {
            this.unauthorizedPrincipalCreator = unauthorizedPrincipalCreator;
        }

        public IPrincipal CreateUnauthenticatedPrincipal()
        {
            return unauthorizedPrincipalCreator.Create();
        }

        public IPrincipal CreatePrincipalFromTicket(FormsAuthenticationTicket ticket)
        {
            throw new NotImplementedException();
        }
    }
}