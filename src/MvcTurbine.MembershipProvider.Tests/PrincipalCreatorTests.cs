using System.Security.Principal;
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
    }
}