using System;
using System.Linq;

namespace MvcTurbine.MembershipProvider
{
    public interface IMembershipService
    {
        bool ValidateUser(string userId, string password);
        void LogInAsUser(string userId, string password);
    }

    public class MembershipService : IMembershipService
    {
        private readonly IPrincipalProvider[] principalProviders;

        public MembershipService(IPrincipalProvider[] principalProviders)
        {
            this.principalProviders = principalProviders;
        }

        public bool ValidateUser(string userId, string password)
        {
            return principalProviders
                .Any(x => x.GetPrincipal(userId, password) != null);
        }

        public void LogInAsUser(string userId, string password)
        {
            throw new NotImplementedException();
        }
    }
}