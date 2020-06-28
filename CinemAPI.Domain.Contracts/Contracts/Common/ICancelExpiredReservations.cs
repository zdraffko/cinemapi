namespace CinemAPI.Domain.Contracts.Contracts.Common
{
    public interface ICancelExpiredReservations
    {
        void Cancel(long projectionId);
    }
}
