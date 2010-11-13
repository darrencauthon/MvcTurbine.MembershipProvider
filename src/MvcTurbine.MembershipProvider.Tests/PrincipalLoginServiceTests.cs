using System.Security.Principal;
using AutoMoq;
using Moq;
using MvcTurbine.MembershipProvider.Contexts;
using NUnit.Framework;

namespace MvcTurbine.MembershipProvider.Tests
{
    [TestFixture]
    public class PrincipalLoginServiceTests
    {
        private AutoMoqer mocker;

        [SetUp]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [Test]
        public void Login_will_pass_the_principal_to_the_current_principal_context()
        {
            var principal = CreateAPrincipal();

            var service = mocker.Resolve<PrincipalLoginService>();
            service.LogIn(principal, typeof(string));

            mocker.GetMock<ICurrentPrincipalContext>()
                .Verify(x => x.Set(principal, typeof(string)), Times.Once());
        }

        private static IPrincipal CreateAPrincipal()
        {
            return new Mock<IPrincipal>().Object;
        }
    }
}