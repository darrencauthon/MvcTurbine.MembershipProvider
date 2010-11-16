using System;
using System.Security.Principal;
using MvcTurbine.MembershipProvider.Contexts;

namespace MvcTurbine.MembershipProvider.Services
{
    public interface IPrincipalLoginService
    {
        void LogIn(IPrincipal principal, Type getType);
    }

    public class PrincipalLoginService : IPrincipalLoginService
    {
        private readonly ICurrentPrincipalContext currentPrincipalContext;

        public PrincipalLoginService(ICurrentPrincipalContext currentPrincipalContext)
        {
            this.currentPrincipalContext = currentPrincipalContext;
        }

        public void LogIn(IPrincipal principal, Type type)
        {
            currentPrincipalContext.Set(principal, type);
        }
    }
}