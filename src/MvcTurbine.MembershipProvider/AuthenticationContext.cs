﻿using System.Security.Principal;
using System.Web.Security;

namespace MvcTurbine.MembershipProvider
{
    public class AuthenticationContext : IAuthenticationContext
    {
        private readonly IPrincipalCreator principalCreator;
        private readonly IPrincipalSetter principalSetter;

        public AuthenticationContext(IPrincipalCreator principalCreator,
                                     IPrincipalSetter principalSetter)
        {
            this.principalCreator = principalCreator;
            this.principalSetter = principalSetter;
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
            principalSetter.SetPricipal(principalCreator.CreateUnauthenticatedPrincipal());
        }

        private void UseThePrincipleFromTheTicket(IPrincipal principal)
        {
            var ticket = GetTheTicketFromThePrincipal(principal);
            var principalFromTicket = principalCreator.CreatePrincipalFromTicket(ticket);
            principalSetter.SetPricipal(principalFromTicket);
        }

        private FormsAuthenticationTicket GetTheTicketFromThePrincipal(IPrincipal principal)
        {
            return ((FormsIdentity) principal.Identity).Ticket;
        }

        private bool ThisIsAValidPrincipal(IPrincipal principal)
        {
            return principal != null && principal.Identity.IsAuthenticated &&
                   principal.Identity.GetType() == typeof (FormsIdentity);
        }
    }
}