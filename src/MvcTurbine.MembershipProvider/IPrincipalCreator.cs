using System.Security.Principal;
using System.Web.Security;

namespace MvcTurbine.MembershipProvider
{
    public interface IPrincipalCreator
    {
        IPrincipal CreateUnauthenticatedPrincipal();
        IPrincipal CreatePrincipalFromTicket(FormsAuthenticationTicket ticket);
    }
}