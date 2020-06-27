using CinemAPI.Domain.Contracts.Models.TicketModels;

namespace CinemAPI.Domain.Contracts.Contracts.TicketContracts
{
    public interface IBuyWithReservation
    {
        BuyWithReservationSummary Handle(long ticketId);
    }
}
