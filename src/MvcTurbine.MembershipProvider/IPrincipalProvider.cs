using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public interface IPrincipalProvider
    {
        PrincipalProviderResult GetPrincipal(string userId, string userData);

        IPrincipal CreatePrincipalFromTicketData(string userName, string userData);

        TicketData ConvertPrincipalToTicketData(IPrincipal principal);
    }
}