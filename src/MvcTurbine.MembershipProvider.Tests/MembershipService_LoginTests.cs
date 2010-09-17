using System.Security.Principal;
using Moq;
using NUnit.Framework;

namespace MvcTurbine.MembershipProvider.Tests
{
    [TestFixture]
    public class MembershipService_LoginTests
    {
        [Test]
        public void Login_passes_the_principal_from_valid_provider_to_principal_login_service()
        {
            var expected = new Mock<IPrincipal>().Object;

            var validPrincipalProvider = new Mock<IPrincipalProvider>();
            validPrincipalProvider.Setup(x => x.GetPrincipal("user name", "password"))
                .Returns(CreateResultWithThisPrincipal(expected));

            var principalLoginService = new Mock<IPrincipalLoginService>();

            var membershipService = new MembershipService(new[] {validPrincipalProvider.Object},
                                                          principalLoginService.Object);
            membershipService.LogInAsUser("user name", "password");

            principalLoginService
                .Verify(x => x.LogIn(expected), Times.Once());
        }

        [Test]
        public void Login_only_passes_a_principal_from_a_valid_provider()
        {
            var expected = new Mock<IPrincipal>().Object;

            var principalLoginService = new Mock<IPrincipalLoginService>();

            var validPrincipalProvider = new Mock<IPrincipalProvider>();
            validPrincipalProvider.Setup(x => x.GetPrincipal("user name", "password"))
                .Returns(CreateResultWithThisPrincipal(expected));

            var invalidPrincipalProvider = new Mock<IPrincipalProvider>();

            var membershipService =
                new MembershipService(new[] {invalidPrincipalProvider.Object, validPrincipalProvider.Object},
                                      principalLoginService.Object);
            membershipService.LogInAsUser("user name", "password");

            principalLoginService
                .Verify(x => x.LogIn(null), Times.Never());
        }

        [Test]
        public void Login_only_passes_one_valid_principal()
        {
            var principalLoginService = new Mock<IPrincipalLoginService>();

            var provider1 = new Mock<IPrincipalProvider>();
            provider1.Setup(x => x.GetPrincipal("user name", "password"))
                .Returns(CreateResultWithThisPrincipal(new Mock<IPrincipal>().Object));

            var provider2 = new Mock<IPrincipalProvider>();
            provider2.Setup(x => x.GetPrincipal("user name", "password"))
                .Returns(CreateResultWithThisPrincipal(new Mock<IPrincipal>().Object));

            var membershipService = new MembershipService(new[] {provider2.Object, provider1.Object},
                                                          principalLoginService.Object);
            membershipService.LogInAsUser("user name", "password");

            principalLoginService
                .Verify(x => x.LogIn(It.IsAny<IPrincipal>()), Times.Once());
        }

        private PrincipalProviderResult CreateResultWithThisPrincipal(IPrincipal principal)
        {
            return new PrincipalProviderResult
                       {
                           Principal = principal,
                       };
        }
    }
}