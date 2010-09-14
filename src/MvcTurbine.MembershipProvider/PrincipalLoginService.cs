using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public interface IPrincipalLoginService
    {
        void LogIn(IPrincipal principal);
    }

    public class PrincipalLoginService : IPrincipalLoginService
    {
        private readonly ICurrentPrincipalContext currentPrincipalContext;

        public PrincipalLoginService(ICurrentPrincipalContext currentPrincipalContext)
        {
            this.currentPrincipalContext = currentPrincipalContext;
        }

        public void LogIn(IPrincipal principal)
        {
            currentPrincipalContext.Set(principal);
        }
    }
}