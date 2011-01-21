MvcTurbine.MembershipProvider
========

MvcTurbine.MembershipProvider is a MVC Turbine plugin that I wrote to handle security and logins.  

It is modeled after the MembershipService class you'll find in the default MVC project.  The interface is as follows:

    public interface IMembershipService
    {
        bool ValidateUser(string userId, string password);
        IPrincipal LogInAsUser(string userId, string password);
    }

So by adding a reference to MvcTurbine.MembershipProvider, you'll be able to ask your IoC container of choice for a membership service and log someone in with a user id and password.

The difference between this membership service and the one in the default MVC project is that the one in MvcTurbine.MembershipProvider is not bound to SQL Server and a ton of stored procedures.  MvcTurbine.MembershipProvider provides the IPrincipalProvider interface that lets you create your own data sources for the membership service.  Giving someone a login to your system can be as simple as:

	public class AynRandGetsAccess : PrincipalProvider {
		public PrincipalProviderResult GetPrincipal(string userId, string password){
			if (userId == "Ayn" && password == "Rand")
				return new PrincipalProviderResult {
					Principal = new GenericPrincipal(new GenericIdentity("Ayn Rand"), new string[] {})
					};
			return new PrincipalProviderResult();
		}
	}

You can create as many principal providers as you wish.  When your MVC application starts up, they will all be registered and any attempt to login will be passed to each provider's GetPrincipal(userId, password) method until a successful match is found.  

MvcTurbine.MembershipProvider will handle everything required to set HttpContext.Current.User to your principal after login, using standard .Net Forms Authentication.  If you want to sign out a user, just call FormsAuthentication.SignOut().

If you want more control over the timeout, the type of principal created, etc., then just create an implementation of IPrincipalProvider.  The interface is:

    public interface IPrincipalProvider
    {
        PrincipalProviderResult GetPrincipal(string userId, string password);
        IPrincipal CreatePrincipalFromTicketData(string userName, string userData);
        TicketData ConvertPrincipalToTicketData(IPrincipal principal);
    }

Implementing these methods will handle everything necessary to convert data from the standard .Net FormsAuthenticationTicket into the appropriate Principal that you need.  MvcTurbine.MembershipProvider will also record which principal provider handled the login, so it will know which principal provider to instantiate and use on every page request when setting HttpContext.Current.User.


