using System.Web;

namespace MvcTurbine.MembershipProvider.HttpModules
{
    public class HandleAuthenticationHttpModule : IHttpModule
    {
        private readonly IAuthenticationContext authenticationContext;

        public HandleAuthenticationHttpModule(IAuthenticationContext authenticationContext)
        {
            this.authenticationContext = authenticationContext;
        }

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest +=
                (sender, e) => authenticationContext.Authenticate(HttpContext.Current.User);
        }

        public void Dispose()
        {
        }
    }
}