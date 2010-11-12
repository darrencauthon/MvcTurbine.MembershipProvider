using System;
using System.Security.Principal;
using System.Web.Security;
using AutoMoq;
using Moq;
using NUnit.Framework;
using Should;

namespace MvcTurbine.MembershipProvider.Tests
{
    [TestFixture]
    public class PrincipalCreatorTests
    {
        private AutoMoqer mocker;

        [SetUp]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [Test]
        public void CreateUnauthenticatedPrincipal_returns_the_principal_from_the_unauthorized_principal_creator()
        {
            var expected = new Mock<IPrincipal>().Object;

            mocker.GetMock<IUnauthenticatedPrincipalCreator>()
                .Setup(x => x.Create())
                .Returns(expected);

            var creator = mocker.Resolve<PrincipalCreator>();
            var result = creator.CreateUnauthenticatedPrincipal();

            result.ShouldBeSameAs(expected);
        }

        [Test]
        public void CreatePrincipalFromTicket_returns_principal_created_from_the_principal_from_ticket_creator()
        {
            var expected = new Mock<IPrincipal>().Object;

            var ticket = CreateTicket();

            mocker.GetMock<IPrincipalFromTicketCreator>()
                .Setup(x => x.Create(ticket))
                .Returns(expected);

            var creator = mocker.Resolve<PrincipalCreator>();
            var result = creator.CreatePrincipalFromTicket(ticket);

            result.ShouldBeSameAs(expected);
        }

        private FormsAuthenticationTicket CreateTicket()
        {
            return new FormsAuthenticationTicket(1, "test", DateTime.Now, DateTime.Now, false, "");
        }
    }
}