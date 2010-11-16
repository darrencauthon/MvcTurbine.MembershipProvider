using System.Security.Principal;
using AutoMoq;
using MvcTurbine.MembershipProvider.Mappers;
using NUnit.Framework;
using Should;

namespace MvcTurbine.MembershipProvider.Tests
{
    [TestFixture]
    public class DefaultTicketDataToPrincipalMapperTests
    {
        private AutoMoqer mocker;

        [SetUp]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [Test]
        public void CanMap_returns_true_when_username_is_not_empty()
        {
            var mapper = mocker.Resolve<DefaultTicketDataToPrincipalMapper>();
            var result = mapper.CanMap("x", string.Empty);

            result.ShouldBeTrue();
        }

        [Test]
        public void CanMap_returns_false_when_username_is_empty()
        {
            var mapper = mocker.Resolve<DefaultTicketDataToPrincipalMapper>();
            var result = mapper.CanMap(string.Empty, string.Empty);

            result.ShouldBeFalse();
        }

        [Test]
        public void CanMap_returns_false_when_username_is_null()
        {
            var mapper = mocker.Resolve<DefaultTicketDataToPrincipalMapper>();
            var result = mapper.CanMap(null, string.Empty);

            result.ShouldBeFalse();
        }

        [Test]
        public void Map_returns_a_generic_principal()
        {
            var mapper = mocker.Resolve<DefaultTicketDataToPrincipalMapper>();
            var result = mapper.Map("x", string.Empty);

            result.ShouldBeType(typeof (GenericPrincipal));
        }

        [Test]
        public void The_identity_on_the_principal_is_generic()
        {
            var mapper = mocker.Resolve<DefaultTicketDataToPrincipalMapper>();
            var result = mapper.Map("x", string.Empty);

            result.Identity.ShouldBeType(typeof (GenericIdentity));
        }

        [Test]
        public void The_name_on_the_identity_is_the_username_from_ticket_data()
        {
            var mapper = mocker.Resolve<DefaultTicketDataToPrincipalMapper>();
            var result = mapper.Map("username", string.Empty);

            result.Identity.Name.ShouldEqual("username");
        }
    }
}