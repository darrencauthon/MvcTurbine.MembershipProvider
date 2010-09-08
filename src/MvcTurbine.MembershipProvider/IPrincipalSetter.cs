using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public interface IPrincipalSetter
    {
        void SetPricipal(IPrincipal principal);
    }
}