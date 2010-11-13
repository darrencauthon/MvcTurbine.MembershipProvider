using System;
using System.Security.Principal;
using System.Web.Security;
using AutoMoq;
using Moq;
using MvcTurbine.MembershipProvider.Contexts;
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
            var expected = CreateAPrincipal();

            ThePrincipalCreatorWillReturnThisAsAnUnauthenticatedPrincipal(expected);

            var context = mocker.Resolve<AuthenticationContext>();
            context.Authenticate(null);

            mocker.GetMock<IPrincipalContext>()
                .Verify(x => x.SetPricipal(expected), Times.Once());
        }

        [Test]
        public void Sets_the_principal_to_one_generated_from_a_ticket_when_passed_a_principal_with_a_ticket()
        {
            var expected = new Mock<IPrincipal>().Object;

            var ticket = CreateTicket("", "");
            SetPrincipalCreatorToReturnThisPrincipalWhenPassedThisTicket(expected, ticket);

            var context = mocker.Resolve<AuthenticationContext>();
            context.Authenticate(CreatePrincipalWithThisTicket(ticket));

            mocker.GetMock<IPrincipalContext>()
                .Verify(x => x.SetPricipal(expected), Times.Once());
        }

        [Test]
        public void Sets_the_principal_only_once_when_a_ticket_exists()
        {
            SetPrincipalCreatorToReturnThisPrincipalWhenPassedThisTicket(CreateAPrincipal(),
                                                                         CreateTicket("", ""));

            var context = mocker.Resolve<AuthenticationContext>();
            context.Authenticate(CreatePrincipalWithThisTicket(CreateTicket("", "")));

            mocker.GetMock<IPrincipalContext>()
                .Verify(x => x.SetPricipal(It.IsAny<IPrincipal>()), Times.Once());
        }

        [Test]
        public void Sets_the_principal_to_an_unauthenticated_principal_when_passed_an_unauthenticated_principal()
        {
            var expected = CreateAPrincipal();

            ThePrincipalCreatorWillReturnThisAsAnUnauthenticatedPrincipal(expected);

            var identityFake = new Mock<IIdentity>();
            identityFake.Setup(x => x.IsAuthenticated).Returns(false);

            var context = mocker.Resolve<AuthenticationContext>();
            context.Authenticate(CreateAPrincipalWithThisIdentity(identityFake));

            mocker.GetMock<IPrincipalContext>()
                .Verify(x => x.SetPricipal(expected), Times.Once());
        }

        [Test]
        public void Sets_the_principal_to_an_unathenticated_principal_when_passed_a_non_FormsIdentity()
        {
            var expected = CreateAPrincipal();

            ThePrincipalCreatorWillReturnThisAsAnUnauthenticatedPrincipal(expected);

            var identityFake = new Mock<IIdentity>();
            identityFake.Setup(x => x.IsAuthenticated).Returns(true);

            var context = mocker.Resolve<AuthenticationContext>();
            context.Authenticate(CreateAPrincipalWithThisIdentity(identityFake));

            mocker.GetMock<IPrincipalContext>()
                .Verify(x => x.SetPricipal(expected), Times.Once());
        }

        private void ThePrincipalCreatorWillReturnThisAsAnUnauthenticatedPrincipal(IPrincipal expected)
        {
            mocker.GetMock<IPrincipalCreator>()
                .Setup(x => x.CreateUnauthenticatedPrincipal())
                .Returns(expected);
        }

        private static IPrincipal CreateAPrincipalWithThisIdentity(Mock<IIdentity> identityFake)
        {
            var principalFake = new Mock<IPrincipal>();
            principalFake.Setup(x => x.Identity)
                .Returns(identityFake.Object);

            return principalFake.Object;
        }

        private static IPrincipal CreateAPrincipal()
        {
            return new Mock<IPrincipal>().Object;
        }

        private void SetPrincipalCreatorToReturnThisPrincipalWhenPassedThisTicket(IPrincipal expected,
                                                                                  FormsAuthenticationTicket ticket)
        {
            mocker.GetMock<IPrincipalCreator>()
                .Setup(x => x.CreatePrincipalFromTicket(ticket))
                .Returns(expected);
        }

        private static IPrincipal CreatePrincipalWithThisTicket(FormsAuthenticationTicket ticket)
        {
            var principalFake = new Mock<IPrincipal>();
            principalFake.Setup(x => x.Identity)
                .Returns(new FormsIdentity(ticket));

            return principalFake.Object;
        }

        private static FormsAuthenticationTicket CreateTicket(string name, string userData)
        {
            return new FormsAuthenticationTicket(1, name, DateTime.Now, DateTime.Now.AddDays(1), false, userData);
        }
    }
}