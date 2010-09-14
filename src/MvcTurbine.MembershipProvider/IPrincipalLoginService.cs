using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public interface IPrincipalLoginService
    {
        void LogIn(IPrincipal principal);
    }
}