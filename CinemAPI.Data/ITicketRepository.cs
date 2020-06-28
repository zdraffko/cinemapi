using System.Threading.Tasks;
using CinemAPI.Models.Contracts.Ticket;
using CinemAPI.Models.DTOs;

namespace CinemAPI.Data
{
    public interface ITicketRepository
    {
        Task<ITicket> GetById(long ticketId);

        Task<ITicket> Get(long projectionId, int row, int column);

        Task Insert(ITicketCreation ticket);

        Task ReserveSeats(long projectionId, int row, int column);

        Task BuyWithoutReservation(long projectionId, int row, int column);

        Task BuyWithReservation(long ticketId);

        Task CancelReservation(long ticketId);

        Task<int> CancelReservationsForProjection(long projectionId);

        Task<FullTicketInfoDto> GetFullTicketInformation(long ticketId);
    }
}
