using System.Security.Principal;
using AutoMoq;
using Moq;
using NUnit.Framework;

namespace MvcTurbine.MembershipProvider.Tests
{
    [TestFixture]
    public class AuthenticationContextTests
    {
        private AutoMoqer mocker;

        [SetUp]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [Test]
        public void Sets_the_principal_to_an_unauthenticated_principal_when_passed_a_null_principal()
        {
            var expected = new Mock<IPrincipal>().Object;

            mocker.GetMock<IPrincipalCreator>()
                .Setup(x => x.CreateUnauthenticatedPrincipal())
                .Returns(expected);

            var context = mocker.Resolve<AuthenticationContext>();
            context.Authenticate(null);

            mocker.GetMock<IPrincipalSetter>()
                .Verify(x => x.SetPricipal(expected), Times.Once());
        }
    }
}