using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public interface IPrincipalProvider
    {
        PrincipalProviderResult GetPrincipal(string userId, string password);
    }

    public class PrincipalProviderResult
    {
        public IPrincipal Principal { get; set; }
    }
}