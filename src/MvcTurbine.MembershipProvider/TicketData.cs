using System;

namespace MvcTurbine.MembershipProvider
{
    public class TicketData
    {
        public string UserData { get; set; }

        public int NumberOfMinutesUntilExpiration { get; set; }

        public bool IsPersistent { get; set; }
    }
}