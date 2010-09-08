using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public interface IUnauthorizedPrincipalCreator
    {
        IPrincipal Create();
    }
}