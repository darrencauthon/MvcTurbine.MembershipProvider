using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public class PrincipalProviderResult
    {
        public IPrincipal Principal { get; set; }

        public bool PrincipalExists
        {
            get { return Principal != null; }
        }
    }
}