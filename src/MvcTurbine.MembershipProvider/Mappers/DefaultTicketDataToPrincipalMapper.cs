using System.Security.Principal;

namespace MvcTurbine.MembershipProvider.Mappers
{
    public class DefaultTicketDataToPrincipalMapper : ITicketDataToPrincipalMapper
    {
        public bool CanMap(string username, string userData)
        {
            return string.IsNullOrEmpty(username) == false;
        }

        public IPrincipal Map(string username, string userData)
        {
            return new GenericPrincipal(new GenericIdentity(username), new string[] {});
        }
    }
}