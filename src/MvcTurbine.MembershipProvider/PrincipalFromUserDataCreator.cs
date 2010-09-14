using System.Linq;
using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public class PrincipalFromUserDataCreator : IPrincipalFromUserDataCreator
    {
        private readonly ITicketDataToPrincipalMapper[] ticketDataToPrincipalMappers;

        public PrincipalFromUserDataCreator(ITicketDataToPrincipalMapper[] ticketDataToPrincipalMappers)
        {
            this.ticketDataToPrincipalMappers = ticketDataToPrincipalMappers;
        }

        public IPrincipal CreatePrincipal(string username, string userData)
        {
            if (ticketDataToPrincipalMappers.Any())
                return ticketDataToPrincipalMappers.First().Map(username, userData);
            return null;
        }
    }
}