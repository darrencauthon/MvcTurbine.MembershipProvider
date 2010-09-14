using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public interface IPrincipalProvider
    {
        IPrincipal GetPrincipal(string userId, string password);
    }
}