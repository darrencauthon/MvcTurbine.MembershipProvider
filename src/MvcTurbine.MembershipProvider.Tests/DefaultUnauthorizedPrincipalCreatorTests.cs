using System.Security.Principal;
using AutoMoq;
using NUnit.Framework;
using Should;

namespace MvcTurbine.MembershipProvider.Tests
{
    [TestFixture]
    public class DefaultUnauthorizedPrincipalCreatorTests
    {
        private AutoMoqer mocker;

        [SetUp]
        public void Setup()
        {
            mocker = new AutoMoqer();
        }

        [Test]
        public void Returns_a_genric_principal()
        {
            var creator = mocker.Resolve<DefaultUnauthenticatedPrincipalCreator>();
            var result = creator.Create();

            result.ShouldBeType(typeof (GenericPrincipal));
        }

        [Test]
        public void The_identity_on_the_principal_is_unauthenticated()
        {
            var creator = mocker.Resolve<DefaultUnauthenticatedPrincipalCreator>();
            var result = creator.Create();

            result.Identity.IsAuthenticated.ShouldBeFalse();
        }
    }
}