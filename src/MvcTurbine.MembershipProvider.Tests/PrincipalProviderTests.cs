using System;
using System.Security.Principal;
using NUnit.Framework;
using Should;

namespace MvcTurbine.MembershipProvider.Tests
{
    [TestFixture]
    public class PrincipalProviderTests
    {
        [Test]
        public void CreatePrincipalFromTicketData_Returns_a_generic_principal_and_identity()
        {
            var principalProvider = new TestingPrincipalProvider();
            var principal = principalProvider.CreatePrincipalFromTicketData("test", "");

            principal.ShouldBeType(typeof (GenericPrincipal));
            principal.Identity.ShouldBeType(typeof (GenericIdentity));
        }

        [Test]
        public void CreatePrincipalFromTicketData_sets_the_identity_name_to_that_which_was_passed_in()
        {
            var principalProvider = new TestingPrincipalProvider();
            var principal = principalProvider.CreatePrincipalFromTicketData("expected", "");

            principal.Identity.Name.ShouldEqual("expected");
        }

        [Test]
        public void ConvertPrincipalToTicketData_returns_ticket_data()
        {
            var principalProvider = new TestingPrincipalProvider();

            var ticketData =
                principalProvider.ConvertPrincipalToTicketData(new GenericPrincipal(new GenericIdentity("x"),
                                                                                    new string[] {}));

            ticketData.ShouldNotBeNull();
        }

        [Test]
        public void ConvertPrincipalToTicketData_sets_username_to_the_name_on_the_identity_that_was_passed_in()
        {
            var principalProvider = new TestingPrincipalProvider();

            var ticketData =
                principalProvider.ConvertPrincipalToTicketData(new GenericPrincipal(new GenericIdentity("expected"),
                                                                                    new string[] {}));

            ticketData.Username.ShouldEqual("expected");
        }
    }

    public class TestingPrincipalProvider : PrincipalProvider
    {
        public override PrincipalProviderResult GetPrincipal(string userId, string password)
        {
            throw new NotImplementedException();
        }
    }
}