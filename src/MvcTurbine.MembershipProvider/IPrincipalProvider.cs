namespace MvcTurbine.MembershipProvider
{
    public interface IPrincipalProvider
    {
        PrincipalProviderResult GetPrincipal(string userId, string password);
    }
}