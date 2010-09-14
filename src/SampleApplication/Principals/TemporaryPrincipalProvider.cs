using System.Security.Principal;
using MvcTurbine.MembershipProvider;

namespace SampleApplication.Principals
{
    public class TemporaryPrincipalProvider : IPrincipalProvider
    {
        public IPrincipal GetPrincipal(string userId, string password)
        {
            return new GenericPrincipal(new GenericIdentity(userId), new string[] {});
        }
    }
}