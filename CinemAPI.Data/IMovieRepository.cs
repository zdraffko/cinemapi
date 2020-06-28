using System.Threading.Tasks;
using CinemAPI.Models.Contracts.Movie;

namespace CinemAPI.Data
{
    public interface IMovieRepository
    {
        Task<IMovie> GetById(int movieId);

        Task<IMovie> GetByNameAndDuration(string name, short duration);

        Task Insert(IMovieCreation movie);
    }
}