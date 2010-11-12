namespace MvcTurbine.MembershipProvider
{
    public interface ITicketDataRetriever
    {
        TicketData GetData();
    }

    public class TicketDataRetriever : ITicketDataRetriever
    {
        public TicketData GetData()
        {
            return new TicketData();
        }
    }

    public class TicketData
    {
    }
}