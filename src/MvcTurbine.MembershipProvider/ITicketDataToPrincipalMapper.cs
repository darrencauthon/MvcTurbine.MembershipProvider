using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public interface ITicketDataToPrincipalMapper
    {
        bool CanMap(string username, string userData);
        IPrincipal Map(string username, string userData);
    }
}