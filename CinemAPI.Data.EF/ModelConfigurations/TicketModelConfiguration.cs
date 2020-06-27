using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using CinemAPI.Models;

namespace CinemAPI.Data.EF.ModelConfigurations
{
    public class TicketModelConfiguration : IModelConfiguration
    {
        public void Configure(DbModelBuilder modelBuilder)
        {
            EntityTypeConfiguration<Ticket> ticketModel = modelBuilder.Entity<Ticket>();

            ticketModel.HasKey(model => model.Id);

            ticketModel.Property(model => model.ProjectionStartDate).IsRequired();
            ticketModel.Property(model => model.MovieName).IsRequired().HasMaxLength(200);
            ticketModel.Property(model => model.CinemaName).IsRequired().HasMaxLength(200);
            ticketModel.Property(model => model.RoomNumber).IsRequired();
            ticketModel.Property(model => model.Row).IsRequired();
            ticketModel.Property(model => model.Column).IsRequired();
            ticketModel.Property(model => model.IsReserved).IsRequired();
            ticketModel.Property(model => model.IsBought).IsRequired();

            ticketModel.HasIndex(model => new
            {
                model.ProjectionStartDate,
                model.MovieName,
                model.CinemaName,
                model.RoomNumber,
                model.Row,
                model.Column
            }).IsUnique();
        }
    }
}
