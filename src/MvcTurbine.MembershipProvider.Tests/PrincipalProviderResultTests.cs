using System.Security.Principal;
using Moq;
using NUnit.Framework;
using Should;

namespace MvcTurbine.MembershipProvider.Tests
{
    [TestFixture]
    public class PrincipalProviderResultTests
    {
        [Test]
        public void PrincipalExists_is_false_when_the_principal_is_null()
        {
            var principalProviderResult = new PrincipalProviderResult {Principal = null};
            principalProviderResult.PrincipalExists.ShouldBeFalse();
        }

        [Test]
        public void PrincipalExists_is_true_when_the_principal_is_not_null()
        {
            var principalProviderResult = new PrincipalProviderResult {Principal = new Mock<IPrincipal>().Object};
            principalProviderResult.PrincipalExists.ShouldBeTrue();
        }
    }
}