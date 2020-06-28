using System.Threading.Tasks;
using CinemAPI.Domain.Contracts.Models.TicketModels;

namespace CinemAPI.Domain.Contracts.Contracts.TicketContracts
{
    public interface IReserveSeats
    {
        Task<ReserveSeatsSummary> Handle(long projectionId, int row, int column);
    }
}
