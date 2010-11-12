using System.Web.Security;
using MvcTurbine.MembershipProvider.Helpers;

namespace MvcTurbine.MembershipProvider
{
    public class DefaultFormsAuthenticationTicketCreator
    {
        public FormsAuthenticationTicket CreateFormsAuthenticationTicket(string userName, string userData)
        {
            var now = CurrentDateTime.Now;
            return new FormsAuthenticationTicket(1, userName,
                                                 now,
                                                 now.AddMinutes(2880),
                                                 true,
                                                 userData,
                                                 FormsAuthentication.FormsCookiePath);
        }
    }
}