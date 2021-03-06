﻿using CinemAPI.Domain.Contracts.Contracts.ProjectionContracts;
using CinemAPI.Domain.Projections.GetAvailableSeatsCount;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace CinemAPI.IoCContainer.DomainPackages.Projections
{
    public class GetAvailableSeatsCountPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<IGetAvailableSeatsCount, GetAvailableSeatsCountHandler>();
            container.RegisterDecorator<IGetAvailableSeatsCount, ProjectionValidation>();
        }
    }
}
