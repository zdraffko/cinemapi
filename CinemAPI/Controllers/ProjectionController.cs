using System;
using System.Threading.Tasks;
using CinemAPI.Models;
using CinemAPI.Models.Input.Projection;
using System.Web.Http;
using CinemAPI.Domain.Contracts.Contracts.ProjectionContracts;
using CinemAPI.Domain.Contracts.Models.ProjectionModels;

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
        public async Task<IHttpActionResult> Index(ProjectionCreationModel model)
        {
            NewProjectionSummary summary;

            try
            {
                summary = await newProj.New(new Projection(
                    model.MovieId,
                    model.RoomId,
                    model.StartDate,
                    model.AvailableSeatsCount,
                    model.IsReservable));
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong while creating a projection.");
            }

            if (summary.IsCreated)
            {
                return Ok();
            }

            return BadRequest(summary.Message);
        }

        [HttpGet]
        public async Task<IHttpActionResult> AvailableSeats(long id)
        {
            GetAvailableSeatsCountSummary summary;

            try
            {
                summary = await getAvailableSeatsCount.Handle(id);
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong.");
            }

            if (!summary.IsValid)
            {
                return BadRequest(summary.Message);
            }

            return Ok(summary.AvailableSeats);
        }
    }
}