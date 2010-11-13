namespace MvcTurbine.MembershipProvider
{
    public interface IMembershipService
    {
        bool ValidateUser(string userId, string password);
        void LogInAsUser(string userId, string password);
    }
}