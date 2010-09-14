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
        private readonly IPrincipalLoginService principalLoginService;

        public MembershipService(IPrincipalProvider[] principalProviders,
                                 IPrincipalLoginService principalLoginService)
        {
            this.principalProviders = principalProviders;
            this.principalLoginService = principalLoginService;
        }

        public bool ValidateUser(string userId, string password)
        {
            return principalProviders
                .Any(x => x.GetPrincipal(userId, password) != null);
        }

        public void LogInAsUser(string userId, string password)
        {
            foreach (var principalProvider in principalProviders)
            {
                var principal = principalProvider.GetPrincipal(userId, password);
                if (principal == null) continue;
                principalLoginService.LogIn(principal);
                break;
            }
        }
    }
}