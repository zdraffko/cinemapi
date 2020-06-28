using System.Threading.Tasks;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Cinema;

namespace CinemAPI.Data
{
    public interface ICinemaRepository
    {
        Task<ICinema> GetById(int cinemaId);

        Task<ICinema> GetByNameAndAddress(string name, string address);

        Task Insert(ICinemaCreation cinema);
    }
}