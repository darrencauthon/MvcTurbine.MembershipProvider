using System.Security.Principal;
using System.Web;

namespace MvcTurbine.MembershipProvider
{
    public interface IPrincipalSetter
    {
        void SetPricipal(IPrincipal principal);
    }

    public class PrincipalSetter : IPrincipalSetter
    {
        public void SetPricipal(IPrincipal principal)
        {
            HttpContext.Current.User = principal;
        }
    }
}