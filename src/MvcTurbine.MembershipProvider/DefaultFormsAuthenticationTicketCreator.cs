using System;
using System.Web.Security;

namespace MvcTurbine.MembershipProvider
{
    public class DefaultFormsAuthenticationTicketCreator
    {
        public FormsAuthenticationTicket CreateFormsAuthenticationTicket(string userName, string userData)
        {
            return new FormsAuthenticationTicket(1, userName,
                                                 DateTime.Now,
                                                 DateTime.Now.AddMinutes(2880),
                                                 true,
                                                 userData,
                                                 FormsAuthentication.FormsCookiePath);
        }
    }
}