using System.Security.Principal;
using Moq;
using NUnit.Framework;
using Should;

namespace MvcTurbine.MembershipProvider.Tests
{
    [TestFixture]
    public class PrincipalFromUserDataCreatorTests
    {
        [Test]
        public void CreatePrincipal_returns_null_when_there_are_no_principal_providers()
        {
            var creator = new PrincipalFromUserDataCreator(new ITicketDataToPrincipalMapper[] {});
            var result = creator.CreatePrincipal(string.Empty, string.Empty);

            result.ShouldBeNull();
        }

        [Test]
        public void CreatePrincipal_returns_a_principal_from_a_valid_mapper()
        {
            var expected = CreatePrincipal();

            var mapper = CreateAMapperThatReturnsThisPrincipalFromThisData(expected, "username", "user data");

            var creator = new PrincipalFromUserDataCreator(new[] {mapper});
            var result = creator.CreatePrincipal("username", "user data");

            result.ShouldBeSameAs(expected);
        }

        private ITicketDataToPrincipalMapper CreateAMapperThatReturnsThisPrincipalFromThisData(IPrincipal expected,
                                                                                               string username,
                                                                                               string userData)
        {
            var mock = new Mock<ITicketDataToPrincipalMapper>();
            mock.Setup(x => x.CanMap(username, userData))
                .Returns(true);
            mock.Setup(x => x.Map(username, userData))
                .Returns(expected);
            return mock.Object;
        }

        private IPrincipal CreatePrincipal()
        {
            return new Mock<IPrincipal>().Object;
        }
    }
}