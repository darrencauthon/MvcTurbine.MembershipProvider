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
    public class PrincipalFromTicketCreatorTests
    {
        private AutoMoqer mocker;

        [SetUp]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [Test]
        public void Create_passes_data_from_the_ticket_to_the_principal_creator_and_returns_the_result()
        {
            var expected = CreateTestPrincipal();

            var ticket = CreateTicket("username", "user data");
            mocker.GetMock<IPrincipalFromUserDataCreator>()
                .Setup(x => x.CreatePrincipal("username", "user data"))
                .Returns(expected);

            var creator = mocker.Resolve<PrincipalFromTicketCreator>();
            var result = creator.Create(ticket);

            result.ShouldBeSameAs(expected);
        }

        private FormsAuthenticationTicket CreateTicket(string username, string userData)
        {
            return new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now, true, userData);
        }

        private static IPrincipal CreateTestPrincipal()
        {
            return new Mock<IPrincipal>().Object;
        }
    }
}