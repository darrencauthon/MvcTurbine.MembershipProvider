using NUnit.Framework;
using Should;

namespace MvcTurbine.MembershipProvider.Tests
{
    [TestFixture]
    public class TicketDataTests
    {
        [Test]
        public void The_default_timeout_should_be_eight_hours()
        {
            new TicketData().NumberOfMinutesUntilExpiration.ShouldEqual(480);
        }
    }
}