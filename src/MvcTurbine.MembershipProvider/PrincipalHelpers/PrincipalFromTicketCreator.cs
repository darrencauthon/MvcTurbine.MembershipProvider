using System;
using System.Security.Principal;
using System.Web.Security;
using MvcTurbine.ComponentModel;

namespace MvcTurbine.MembershipProvider.PrincipalHelpers
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
            var principalProvider = GetThePrincipalProvider(ticket);
            return principalProvider.CreatePrincipalFromTicketData(ticket.Name, GetTheUserDataOutOfTheTicket(ticket));
        }

        private static string GetTheUserDataOutOfTheTicket(FormsAuthenticationTicket ticket)
        {
            return GetTheUserDataArray(ticket)[1];
        }

        private IPrincipalProvider GetThePrincipalProvider(FormsAuthenticationTicket ticket)
        {
            var type = GetThePrincipalProviderType(ticket);
            return serviceLocator.Resolve(type) as IPrincipalProvider;
        }

        private static Type GetThePrincipalProviderType(FormsAuthenticationTicket ticket)
        {
            return Type.GetType(GetTheUserDataArray(ticket)[0]);
        }

        private static string[] GetTheUserDataArray(FormsAuthenticationTicket ticket)
        {
            return ticket.UserData.Split("|".ToCharArray(), 2);
        }
    }
}