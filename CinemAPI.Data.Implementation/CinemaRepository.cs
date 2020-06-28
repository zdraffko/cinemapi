using System.Data.Entity;
using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Cinema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly CinemaDbContext db;

        public CinemaRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<ICinema> GetById(int cinemaId)
        {
            return await db.Cinemas.FirstOrDefaultAsync(c => c.Id == cinemaId);
        }

        public async Task<ICinema> GetByNameAndAddress(string name, string address)
        {
            return await db.Cinemas.Where(x => x.Name == name &&
                                         x.Address == address)
                             .FirstOrDefaultAsync();
        }

        public async Task Insert(ICinemaCreation cinema)
        {
            Cinema newCinema = new Cinema(cinema.Name, cinema.Address);

            db.Cinemas.Add(newCinema);

            await db.SaveChangesAsync();
        }
    }
}