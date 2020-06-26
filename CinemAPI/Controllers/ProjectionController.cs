using CinemAPI.Domain.Contracts;
using CinemAPI.Domain.Contracts.Models;
using CinemAPI.Models;
using CinemAPI.Models.Input.Projection;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class ProjectionController : ApiController
    {
        private readonly INewProjection newProj;
        private readonly IGetAvailableSeatsCount getAvailableSeatsCount;

        public ProjectionController(INewProjection newProj, IGetAvailableSeatsCount getAvailableSeatsCount)
        {
            this.newProj = newProj;
            this.getAvailableSeatsCount = getAvailableSeatsCount;
        }

        [HttpPost]
        public IHttpActionResult Index(ProjectionCreationModel model)
        {
            NewProjectionSummary summary = newProj.New(new Projection(
                model.MovieId,
                model.RoomId,
                model.StartDate,
                model.AvailableSeatsCount));

            if (summary.IsCreated)
            {
                return Ok();
            }
            else
            {
                return BadRequest(summary.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult AvailableSeats(int id)
        {
            GetAvailableSeatsCountSummary summary = getAvailableSeatsCount.Handle(id);

            if (!summary.IsValid)
            {
                return BadRequest(summary.Message);
            }

            return Ok(summary.AvailableSeats);
        }
    }
}