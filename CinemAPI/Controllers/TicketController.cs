using System.Web.Http;
using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Contracts.Models.TicketModels;
using CinemAPI.Models.Input.Ticket;

namespace CinemAPI.Controllers
{
    public class TicketController : ApiController
    {
        private readonly IReserveSeats reserveSeats;

        public TicketController(IReserveSeats reserveSeats)
        {
            this.reserveSeats = reserveSeats;
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
    }
}