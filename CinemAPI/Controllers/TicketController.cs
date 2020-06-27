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

        public TicketController(IReserveSeats reserveSeats, IBuyWithoutReservation buyWithoutReservation)
        {
            this.reserveSeats = reserveSeats;
            this.buyWithoutReservation = buyWithoutReservation;
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
    }
}