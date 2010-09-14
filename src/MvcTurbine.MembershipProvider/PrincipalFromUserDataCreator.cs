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
            var mapper = GetAWorkingMapper(username, userData);

            return NoWorkingMapperExists(mapper)
                       ? null
                       : mapper.Map(username, userData);
        }

        private static bool NoWorkingMapperExists(ITicketDataToPrincipalMapper mapper)
        {
            return mapper == null;
        }

        private ITicketDataToPrincipalMapper GetAWorkingMapper(string username, string userData)
        {
            return ticketDataToPrincipalMappers
                .FirstOrDefault(x => x.CanMap(username, userData));
        }
    }
}