using System.Security.Principal;
using Moq;
using NUnit.Framework;
using Should;

namespace MvcTurbine.MembershipProvider.Tests
{
    [TestFixture]
    public class MembershipServiceTests
    {
        [Test]
        public void Returns_true_when_a_principal_provider_returns_a_principal()
        {
            var principalProvider = new Mock<IPrincipalProvider>();
            principalProvider.Setup(x => x.GetPrincipal("username", "password"))
                .Returns(new Mock<IPrincipal>().Object);

            var service = new MembershipService(new[] {principalProvider.Object});
            var result = service.ValidateUser("username", "password");

            result.ShouldBeTrue();
        }

        [Test]
        public void Returns_false_when_a_principal_provider_does_not_return_a_principal()
        {
            var principalProvider = new Mock<IPrincipalProvider>();
            principalProvider.Setup(x => x.GetPrincipal("username", "password"))
                .Returns((string username, string password) => null);

            var service = new MembershipService(new[] {principalProvider.Object});
            var result = service.ValidateUser("username", "password");

            result.ShouldBeFalse();
        }

        [Test]
        public void Returns_false_when_no_principal_providers_exist()
        {
            var principalProvider = new Mock<IPrincipalProvider>();
            principalProvider.Setup(x => x.GetPrincipal("username", "password"))
                .Returns((string username, string password) => null);

            var service = new MembershipService(new IPrincipalProvider[] {});
            var result = service.ValidateUser("username", "password");

            result.ShouldBeFalse();
        }
    }
}