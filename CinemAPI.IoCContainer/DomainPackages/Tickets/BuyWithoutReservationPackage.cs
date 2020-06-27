using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Tickets.BuyWithoutReservation;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace CinemAPI.IoCContainer.DomainPackages.Tickets
{
    public class BuyWithoutReservationPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IBuyWithoutReservation, BuyWithoutReservationHandler>();
            container.RegisterDecorator<IBuyWithoutReservation, SeatsValidation>();
            container.RegisterDecorator<IBuyWithoutReservation, ProjectionValidation>();
        }
    }
}
