namespace CinemAPI.Models.Contracts.Ticket
{
    public interface ITicket
    {
        long Id { get; set; }

        long ProjectionId { get; set; }

        int Row { get; set; }

        int Column { get; set; }

        bool IsReserved { get; set; }

        bool IsBought { get; set; }
    }
}
