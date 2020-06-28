using System;
using System.Threading.Tasks;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.Common;
using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Contracts.Models.TicketModels;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Ticket;
using static CinemAPI.Domain.Constants.ReservationConstants;

namespace CinemAPI.Domain.Tickets.BuyWithReservation
{
    public class ReservationValidation : IBuyWithReservation
    {
        private readonly IBuyWithReservation buyWithReservation;
        private readonly ITicketRepository ticketsRepo;
        private readonly IProjectionRepository projectionsRepo;
        private readonly ICancelExpiredReservations cancelExpiredReservations;

        public ReservationValidation(
            IBuyWithReservation buyWithReservation,
            ITicketRepository ticketsRepo,
            IProjectionRepository projectionsRepo,
            ICancelExpiredReservations cancelExpiredReservations)
        {
            this.buyWithReservation = buyWithReservation;
            this.ticketsRepo = ticketsRepo;
            this.projectionsRepo = projectionsRepo;
            this.cancelExpiredReservations = cancelExpiredReservations;
        }

        public async Task<BuyWithReservationSummary> Handle(long ticketId)
        {
            ITicket ticket = await ticketsRepo.GetById(ticketId);

            if (ticket == null)
            {
                return new BuyWithReservationSummary($"A reservation with Id {ticketId} does not exist.");
            }

            await cancelExpiredReservations.Cancel(ticket.ProjectionId);
            IProjection projection = await projectionsRepo.GetById(ticket.ProjectionId);

            if (projection.StartDate - TimeSpan.FromMinutes(MinutesUntilReservationExpires) <= DateTime.UtcNow)
            {
                return new BuyWithReservationSummary($"The reservation with Id {ticketId} has expired.");
            }

            if (ticket.IsBought)
            {
                return new BuyWithReservationSummary($"The ticket with a reservation Id {ticketId} has already been bought.");
            }

            if (!ticket.IsReserved)
            {
                return new BuyWithReservationSummary($"The reservation with Id {ticketId} has been canceled.");
            }

            return await buyWithReservation.Handle(ticketId);
        }
    }
}
