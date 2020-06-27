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
        public IHttpActionResult Reserve(ReserveSeatsModel model)
        {
            ReserveSeatsSummary summary = reserveSeats.Handle(model.ProjectionId, model.Row, model.Column);

            if (!summary.IsSuccessful)
            {
                return BadRequest(summary.Message);
            }

            return Ok(summary.TicketDto);
        }

        [HttpPost]
        public IHttpActionResult Buy(BuyWithoutReservationModel model)
        {
            BuyWithoutReservationSummary summary = buyWithoutReservation.Handle(model.ProjectionId, model.Row, model.Column);

            if (!summary.IsSuccessful)
            {
                return BadRequest(summary.Message);
            }

            return Ok(summary.TicketDto);
        }

        [HttpPost]
        public IHttpActionResult Buy(long id)
        {
            BuyWithReservationSummary summary = buyWithReservation.Handle(id);

            if (!summary.IsSuccessful)
            {
                return BadRequest(summary.Message);
            }

            return Ok(summary.TicketDto);
        }
    }
}