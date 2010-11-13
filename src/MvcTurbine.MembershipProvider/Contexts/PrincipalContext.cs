using System.Security.Principal;
using System.Web;

namespace MvcTurbine.MembershipProvider.Contexts
{
    public interface IPrincipalContext
    {
        void SetPricipal(IPrincipal principal);
    }

    public class PrincipalContext : IPrincipalContext
    {
        public void SetPricipal(IPrincipal principal)
        {
            HttpContext.Current.User = principal;
        }
    }
}