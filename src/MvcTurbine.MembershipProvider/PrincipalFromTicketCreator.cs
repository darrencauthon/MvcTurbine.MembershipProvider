using System.Security.Principal;
using System.Web.Security;
using MvcTurbine.ComponentModel;

namespace MvcTurbine.MembershipProvider
{
    public interface IPrincipalFromTicketCreator
    {
        IPrincipal Create(FormsAuthenticationTicket ticket);
    }

    public class PrincipalFromTicketCreator : IPrincipalFromTicketCreator
    {
        private readonly IServiceLocator serviceLocator;
        private readonly IPrincipalFromUserDataCreator principalFromUserDataCreator;

        public PrincipalFromTicketCreator(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public IPrincipal Create(FormsAuthenticationTicket ticket)
        {
            var userDataArray = ticket.UserData.Split("|".ToCharArray(), 2);
            var type = System.Type.GetType(userDataArray[0]);
            var principalProvider = serviceLocator.Resolve(type) as IPrincipalProvider;
            return principalProvider.CreatePrincipalFromTicketData(ticket.Name, userDataArray[1]);
            //return principalFromUserDataCreator.CreatePrincipal(ticket.Name, ticket.UserData);
        }
    }
}