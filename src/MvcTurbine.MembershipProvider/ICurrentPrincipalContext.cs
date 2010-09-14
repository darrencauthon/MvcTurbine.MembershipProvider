using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public interface ICurrentPrincipalContext
    {
        void Set(IPrincipal principal);
    }
}