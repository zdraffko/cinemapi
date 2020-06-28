using System.Threading.Tasks;

namespace CinemAPI.Domain.Contracts.Contracts.Common
{
    public interface ICancelExpiredReservations
    {
        Task Cancel(long projectionId);
    }
}
