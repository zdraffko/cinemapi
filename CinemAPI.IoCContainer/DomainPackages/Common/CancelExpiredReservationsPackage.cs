using CinemAPI.Domain.Common.CancelExpiredReservations;
using CinemAPI.Domain.Contracts.Contracts.Common;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace CinemAPI.IoCContainer.DomainPackages.Common
{
    public class CancelExpiredReservationsPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ICancelExpiredReservations, CancelExpiredReservations>();
            container.RegisterDecorator<ICancelExpiredReservations, ProjectionValidation>();
        }
    }
}
