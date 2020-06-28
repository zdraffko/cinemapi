using System.Threading.Tasks;
using CinemAPI.Models.Contracts.Room;

namespace CinemAPI.Data
{
    public interface IRoomRepository
    {
        Task<IRoom> GetById(int id);

        Task<IRoom> GetByCinemaAndNumber(int cinemaId, int number);

        Task Insert(IRoomCreation room);
    }
}