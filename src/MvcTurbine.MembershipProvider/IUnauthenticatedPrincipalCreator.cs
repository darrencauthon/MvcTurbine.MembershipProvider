using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public interface IUnauthenticatedPrincipalCreator
    {
        IPrincipal Create();
    }
}