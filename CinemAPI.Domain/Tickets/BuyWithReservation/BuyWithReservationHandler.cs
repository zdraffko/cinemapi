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
        private readonly IProjectionRepository projectionsRepo;

        public BuyWithReservationHandler(
            ITicketRepository ticketsRepo,
            IProjectionRepository projectionsRepo)
        {
            this.ticketsRepo = ticketsRepo;
            this.projectionsRepo = projectionsRepo;
        }

        public BuyWithReservationSummary Handle(long ticketId)
        {
            ticketsRepo.BuyWithReservation(ticketId);

            ITicket ticket = ticketsRepo.GetById(ticketId);

            projectionsRepo.DecreaseAvailableSeats(ticket.ProjectionId, 1);
            FullTicketInfoDto ticketInfo = ticketsRepo.GetFullTicketInformation(ticketId);

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
