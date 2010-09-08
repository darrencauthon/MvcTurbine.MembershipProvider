using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public class DefaultUnauthorizedPrincipalCreator : IUnauthorizedPrincipalCreator
    {
        public IPrincipal Create()
        {
            return new GenericPrincipal(CreateUnauthenticatedIdentity(), NoRoles());
        }

        private static string[] NoRoles()
        {
            return new[] {string.Empty};
        }

        private static GenericIdentity CreateUnauthenticatedIdentity()
        {
            return new GenericIdentity(string.Empty);
        }
    }
}