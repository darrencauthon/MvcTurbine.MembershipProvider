using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public interface IPrincipalCreator
    {
        IPrincipal CreateUnauthenticatedPrincipal();
    }
}