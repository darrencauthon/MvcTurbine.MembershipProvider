using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public abstract class PrincipalProvider : IPrincipalProvider
    {
        public abstract PrincipalProviderResult GetPrincipal(string userId, string password);

        public virtual IPrincipal CreatePrincipalFromTicketData(string userName, string userData)
        {
            return new GenericPrincipal(new GenericIdentity(userName), new string[] {});
        }

        public virtual TicketData ConvertPrincipalToTicketData(IPrincipal principal)
        {
            return new TicketData {Username = principal.Identity.Name};
        }
    }
}