namespace MvcTurbine.MembershipProvider
{
    public class TicketData
    {
        public TicketData()
        {
            NumberOfMinutesUntilExpiration = 480;
        }

        public string UserData { get; set; }

        public int NumberOfMinutesUntilExpiration { get; set; }

        public bool IsPersistent { get; set; }

        public string Username { get; set; }
    }
}