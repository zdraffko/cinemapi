using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Data
{
    public interface ITicketRepository
    {
        ITicket GetById(long ticketId);

        ITicket Get(long projectionId, int row, int column);

        void Insert(ITicketCreation ticket);

        void ReserveSeats(long projectionId, int row, int column);

        void BuyWithoutReservation(long projectionId, int row, int column);

        void BuyWithReservation(long ticketId);

        void CancelReservation(long ticketId);

        int CancelReservationsForProjection(long projectionId);
    }
}
