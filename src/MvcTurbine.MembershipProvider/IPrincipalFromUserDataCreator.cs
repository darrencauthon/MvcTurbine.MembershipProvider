using System.Security.Principal;

namespace MvcTurbine.MembershipProvider
{
    public interface IPrincipalFromUserDataCreator
    {
        IPrincipal CreatePrincipal(string username, string userData);
    }
}