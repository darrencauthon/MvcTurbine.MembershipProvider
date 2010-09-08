using System;
using System.Security.Principal;
using System.Web.Security;

namespace MvcTurbine.MembershipProvider
{
    public class PrincipalCreator : IPrincipalCreator
    {
        private readonly IUnauthenticatedPrincipalCreator unauthenticatedPrincipalCreator;

        public PrincipalCreator(IUnauthenticatedPrincipalCreator unauthenticatedPrincipalCreator)
        {
            this.unauthenticatedPrincipalCreator = unauthenticatedPrincipalCreator;
        }

        public IPrincipal CreateUnauthenticatedPrincipal()
        {
            return unauthenticatedPrincipalCreator.Create();
        }

        public IPrincipal CreatePrincipalFromTicket(FormsAuthenticationTicket ticket)
        {
            throw new NotImplementedException();
        }
    }
}