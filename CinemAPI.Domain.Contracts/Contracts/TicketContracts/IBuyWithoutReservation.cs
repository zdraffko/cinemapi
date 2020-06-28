using System.Threading.Tasks;
using CinemAPI.Domain.Contracts.Models.TicketModels;

namespace CinemAPI.Domain.Contracts.Contracts.TicketContracts
{
    public interface IBuyWithoutReservation
    {
        Task<BuyWithoutReservationSummary> Handle(long projectionId, int row, int column);
    }
}
