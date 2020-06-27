using CinemAPI.Models;
using CinemAPI.Models.Contracts.Cinema;

namespace CinemAPI.Data
{
    public interface ICinemaRepository
    {
        ICinema GetById(int cinemaId);

        ICinema GetByNameAndAddress(string name, string address);

        void Insert(ICinemaCreation cinema);
    }
}