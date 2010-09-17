using System.Security.Principal;
using MvcTurbine.MembershipProvider;

namespace SampleApplication.Principals
{
    public class TemporaryPrincipalProvider : IPrincipalProvider
    {
        public PrincipalProviderResult GetPrincipal(string userId, string password)
        {
            return new PrincipalProviderResult
                       {
                           Principal = new GenericPrincipal(new GenericIdentity(userId), new string[] {})
                       }
                ;
        }
    }
}