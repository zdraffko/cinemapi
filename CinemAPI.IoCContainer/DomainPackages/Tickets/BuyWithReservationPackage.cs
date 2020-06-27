using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Tickets.BuyWithReservation;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace CinemAPI.IoCContainer.DomainPackages.Tickets
{
    public class BuyWithReservationPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IBuyWithReservation, BuyWithReservationHandler>();
            container.RegisterDecorator<IBuyWithReservation, ReservationValidation>();
        }
    }
}
