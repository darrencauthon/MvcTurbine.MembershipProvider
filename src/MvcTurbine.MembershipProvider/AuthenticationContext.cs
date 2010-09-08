using System.Security.Principal;

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
            principalSetter.SetPricipal(principalCreator.CreateUnauthenticatedPrincipal());
        }
    }
}