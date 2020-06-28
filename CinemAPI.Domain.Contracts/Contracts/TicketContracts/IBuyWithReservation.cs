using System.Threading.Tasks;
using CinemAPI.Domain.Contracts.Models.TicketModels;

namespace CinemAPI.Domain.Contracts.Contracts.TicketContracts
{
    public interface IBuyWithReservation
    {
        Task<BuyWithReservationSummary> Handle(long ticketId);
    }
}
