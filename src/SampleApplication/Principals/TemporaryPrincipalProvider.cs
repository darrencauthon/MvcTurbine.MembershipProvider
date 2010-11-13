using System;
using System.Security.Principal;
using MvcTurbine.MembershipProvider;

namespace SampleApplication.Principals
{
    public class TemporaryPrincipalProvider : IPrincipalProvider
    {
        public PrincipalProviderResult GetPrincipal(string userId, string userData)
        {
            return new PrincipalProviderResult
                       {
                           Principal = new GenericPrincipal(new GenericIdentity(userId), new string[] {})
                       };
        }

        public IPrincipal CreatePrincipalFromTicketData(string userName, string userData)
        {
            return new GenericPrincipal(new GenericIdentity(userName), new string[] {});
        }

        public TicketData ConvertPrincipalToTicketData(IPrincipal principal)
        {
            throw new NotImplementedException();
        }
    }
}