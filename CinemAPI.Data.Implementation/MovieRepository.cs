using System.Data.Entity;
using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Movie;
using System.Threading.Tasks;

namespace CinemAPI.Data.Implementation
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaDbContext db;

        public MovieRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<IMovie> GetById(int movieId)
        {
            return await db.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
        }

        public async Task<IMovie> GetByNameAndDuration(string name, short duration)
        {
            return await db.Movies.FirstOrDefaultAsync(x => x.Name == name &&
                                                 x.DurationMinutes == duration);
        }

        public async Task Insert(IMovieCreation movie)
        {
            Movie newMovie = new Movie(movie.Name, movie.DurationMinutes);

            db.Movies.Add(newMovie);
            await db.SaveChangesAsync();
        }
    }
}