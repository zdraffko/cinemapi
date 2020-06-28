using System;
using System.Threading.Tasks;
using System.Web.Http;
using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Contracts.Models.TicketModels;
using CinemAPI.Models.Input.Ticket;

namespace CinemAPI.Controllers
{
    public class TicketController : ApiController
    {
        private readonly IReserveSeats reserveSeats;
        private readonly IBuyWithoutReservation buyWithoutReservation;
        private readonly IBuyWithReservation buyWithReservation;

        public TicketController(
            IReserveSeats reserveSeats,
            IBuyWithoutReservation buyWithoutReservation,
            IBuyWithReservation buyWithReservation)
        {
            this.reserveSeats = reserveSeats;
            this.buyWithoutReservation = buyWithoutReservation;
            this.buyWithReservation = buyWithReservation;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Reserve(ReserveSeatsModel model)
        {
            ReserveSeatsSummary summary;

            try
            {
                summary = await reserveSeats.Handle(model.ProjectionId, model.Row, model.Column);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong during the reservation.");
            }

            if (!summary.IsSuccessful)
            {
                return BadRequest(summary.Message);
            }

            return Ok(summary.TicketDto);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Buy(BuyWithoutReservationModel model)
        {
            BuyWithoutReservationSummary summary;

            try
            {
                summary = await buyWithoutReservation.Handle(model.ProjectionId, model.Row, model.Column);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong while buying a ticket.");
            }

            if (!summary.IsSuccessful)
            {
                return BadRequest(summary.Message);
            }

            return Ok(summary.TicketDto);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Buy(long id)
        {
            BuyWithReservationSummary summary;

            try
            {
                summary = await buyWithReservation.Handle(id);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong while buying a ticket.");
            }

            if (!summary.IsSuccessful)
            {
                return BadRequest(summary.Message);
            }

            return Ok(summary.TicketDto);
        }
    }
}