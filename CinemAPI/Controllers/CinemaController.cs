using System.Threading.Tasks;
using CinemAPI.Data;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Cinema;
using CinemAPI.Models.Input.Cinema;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class CinemaController : ApiController
    {
        private readonly ICinemaRepository cinemaRepo;

        public CinemaController(ICinemaRepository cinemaRepo)
        {
            this.cinemaRepo = cinemaRepo;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Index(CinemaCreationModel model)
        {
            ICinema cinema = await cinemaRepo.GetByNameAndAddress(model.Name, model.Address);

            if (cinema == null)
            {
                await cinemaRepo.Insert(new Cinema(model.Name, model.Address));

                return Ok();
            }

            return BadRequest("Cinema already exists");
        }
    }
}