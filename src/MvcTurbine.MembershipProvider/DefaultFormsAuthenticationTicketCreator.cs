using System;
using System.Security.Principal;
using System.Web.Security;
using MvcTurbine.ComponentModel;
using MvcTurbine.MembershipProvider.Helpers;

namespace MvcTurbine.MembershipProvider
{
    public class DefaultFormsAuthenticationTicketCreator
    {
        private readonly IServiceLocator serviceLocator;

        public DefaultFormsAuthenticationTicketCreator(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public FormsAuthenticationTicket CreateFormsAuthenticationTicket(IPrincipal principal, Type type)
        {

            var principalProvider = serviceLocator.Resolve(type) as IPrincipalProvider;

            var now = CurrentDateTime.Now;
            var ticketData = principalProvider.ConvertPrincipalToTicketData(principal);
            return new FormsAuthenticationTicket(1, principal.Identity.Name,
                                                 now,
                                                 now.AddMinutes(ticketData.NumberOfMinutesUntilExpiration),
                                                 ticketData.IsPersistent,
                                                 type + ", " + type.Assembly.FullName + "|" + ticketData.UserData,
                                                 FormsAuthentication.FormsCookiePath);
        }
    }
}