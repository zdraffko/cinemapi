using System;
using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.Common;
using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Contracts.Models.TicketModels;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Ticket;

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

        public BuyWithReservationSummary Handle(long ticketId)
        {
            ITicket ticket = ticketsRepo.GetById(ticketId);

            if (ticket == null)
            {
                return new BuyWithReservationSummary($"A reservation with Id {ticketId} does not exist.");
            }

            cancelExpiredReservations.Cancel(ticket.ProjectionId);
            IProjection projection = projectionsRepo.GetById(ticket.ProjectionId);

            if (projection.StartDate - TimeSpan.FromMinutes(10) <= DateTime.UtcNow)
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

            return buyWithReservation.Handle(ticketId);
        }
    }
}
