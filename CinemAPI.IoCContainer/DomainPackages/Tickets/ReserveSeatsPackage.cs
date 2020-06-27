using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Tickets.ReserveSeats;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace CinemAPI.IoCContainer.DomainPackages.Tickets
{
    public class ReserveSeatsPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IReserveSeats, ReserveSeatsHandler>();
            container.RegisterDecorator<IReserveSeats, ReservedSeatsValidation>();
            container.RegisterDecorator<IReserveSeats, ExistingSeatsValidation>();
            container.RegisterDecorator<IReserveSeats, ValidProjectionValidation>();
        }
    }
}
