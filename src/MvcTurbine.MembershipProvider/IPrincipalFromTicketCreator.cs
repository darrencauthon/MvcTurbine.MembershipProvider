using System.Security.Principal;
using System.Web.Security;

namespace MvcTurbine.MembershipProvider
{
    public interface IPrincipalFromTicketCreator
    {
        IPrincipal Create(FormsAuthenticationTicket ticket);
    }
}