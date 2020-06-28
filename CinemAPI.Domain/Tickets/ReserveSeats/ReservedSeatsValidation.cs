using System.Threading.Tasks;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Contracts.Models.TicketModels;
using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Domain.Tickets.ReserveSeats
{
    public class ReservedSeatsValidation : IReserveSeats
    {
        private readonly IReserveSeats reserveSeats;
        private readonly ITicketRepository ticketsRepo;

        public ReservedSeatsValidation(
            IReserveSeats reserveSeats,
            ITicketRepository ticketsRepo)
        {
            this.reserveSeats = reserveSeats;
            this.ticketsRepo = ticketsRepo;
        }

        public async Task<ReserveSeatsSummary> Handle(long projectionId, int row, int column)
        {
            ITicket ticket = await ticketsRepo.Get(projectionId, row, column);

            if (ticket != null && ticket.IsBought)
            {
                return new ReserveSeatsSummary($"The seat at row {row} and column {column} is already bought.");
            }

            if (ticket != null && ticket.IsReserved)
            {
                return new ReserveSeatsSummary($"The seat at row {row} and column {column} is already reserved.");
            }

            return await reserveSeats.Handle(projectionId, row, column);
        }
    }
}
