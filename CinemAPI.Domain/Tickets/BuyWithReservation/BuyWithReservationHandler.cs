using System.Threading.Tasks;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Contracts.DTOs;
using CinemAPI.Domain.Contracts.Models.TicketModels;
using CinemAPI.Models.Contracts.Ticket;
using CinemAPI.Models.DTOs;

namespace CinemAPI.Domain.Tickets.BuyWithReservation
{
    public class BuyWithReservationHandler : IBuyWithReservation
    {
        private readonly ITicketRepository ticketsRepo;

        public BuyWithReservationHandler(
            ITicketRepository ticketsRepo)
        {
            this.ticketsRepo = ticketsRepo;
        }

        public async Task<BuyWithReservationSummary> Handle(long ticketId)
        {
            await ticketsRepo.BuyWithReservation(ticketId);
            FullTicketInfoDto ticketInfo = await ticketsRepo.GetFullTicketInformation(ticketId);

            return new BuyWithReservationSummary(new TicketDto
            {
                Id = ticketInfo.Id,
                ProjectionStartDate = ticketInfo.ProjectionStartDate,
                MovieName = ticketInfo.MovieName,
                CinemaName = ticketInfo.CinemaName,
                RoomNumber = ticketInfo.RoomNumber,
                Row = ticketInfo.Row,
                Column = ticketInfo.Column
            });
        }
    }
}
