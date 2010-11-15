using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public interface IMembershipService
    {
        bool ValidateUser(string userId, string password);
        IPrincipal LogInAsUser(string userId, string password);
    }
}