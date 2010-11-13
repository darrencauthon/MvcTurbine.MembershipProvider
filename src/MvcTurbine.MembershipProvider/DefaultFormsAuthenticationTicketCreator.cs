using System;
using System.Security.Principal;
using System.Web.Security;
using MvcTurbine.MembershipProvider.Helpers;

namespace MvcTurbine.MembershipProvider
{
    public class DefaultFormsAuthenticationTicketCreator
    {
        public FormsAuthenticationTicket CreateFormsAuthenticationTicket(IPrincipal principal, Type type)
        {
            var now = CurrentDateTime.Now;
            return new FormsAuthenticationTicket(1, principal.Identity.Name,
                                                 now,
                                                 now.AddMinutes(2880),
                                                 true,
                                                 type + "|" + string.Empty,
                                                 FormsAuthentication.FormsCookiePath);
        }
    }
}