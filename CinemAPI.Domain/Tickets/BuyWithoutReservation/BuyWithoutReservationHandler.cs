using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Contracts.DTOs;
using CinemAPI.Domain.Contracts.Models.TicketModels;
using CinemAPI.Models.Contracts.Ticket;
using CinemAPI.Models.DTOs;

namespace CinemAPI.Domain.Tickets.BuyWithoutReservation
{
    public class BuyWithoutReservationHandler : IBuyWithoutReservation
    {
        private readonly ITicketRepository ticketsRepo;
        private readonly IProjectionRepository projectionsRepo;

        public BuyWithoutReservationHandler(
            ITicketRepository ticketsRepo,
            IProjectionRepository projectionsRepo)
        {
            this.ticketsRepo = ticketsRepo;
            this.projectionsRepo = projectionsRepo; ;
        }

        public BuyWithoutReservationSummary Handle(long projectionId, int row, int column)
        {
            ticketsRepo.BuyWithoutReservation(projectionId, row, column);
            projectionsRepo.DecreaseAvailableSeats(projectionId, 1);

            ITicket newTicket = ticketsRepo.Get(projectionId, row, column);
            FullTicketInfoDto ticketInfo = ticketsRepo.GetFullTicketInformation(newTicket.Id);

            return new BuyWithoutReservationSummary(new TicketDto
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
