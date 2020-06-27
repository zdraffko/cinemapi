﻿using CinemAPI.Data;
using CinemAPI.Domain.Contracts.Contracts.TicketContracts;
using CinemAPI.Domain.Contracts.DTOs;
using CinemAPI.Domain.Contracts.Models.TicketModels;
using CinemAPI.Models.Contracts.Cinema;
using CinemAPI.Models.Contracts.Movie;
using CinemAPI.Models.Contracts.Projection;
using CinemAPI.Models.Contracts.Room;
using CinemAPI.Models.Contracts.Ticket;

namespace CinemAPI.Domain.Tickets.BuyWithoutReservation
{
    public class BuyWithoutReservationHandler : IBuyWithoutReservation
    {
        private readonly ITicketRepository ticketsRepo;
        private readonly IProjectionRepository projectionsRepo;
        private readonly IMovieRepository moviesRepo;
        private readonly IRoomRepository roomsRepo;
        private readonly ICinemaRepository cinemasRepo;

        public BuyWithoutReservationHandler(
            ITicketRepository ticketsRepo,
            IProjectionRepository projectionsRepo,
            IMovieRepository moviesRepo,
            IRoomRepository roomsRepo,
            ICinemaRepository cinemasRepo)
        {
            this.ticketsRepo = ticketsRepo;
            this.projectionsRepo = projectionsRepo;
            this.moviesRepo = moviesRepo;
            this.roomsRepo = roomsRepo;
            this.cinemasRepo = cinemasRepo;
        }

        public BuyWithoutReservationSummary Handle(long projectionId, int row, int column)
        {
            ticketsRepo.BuyWithoutReservation(projectionId, row, column);
            projectionsRepo.DecreaseAvailableSeats(projectionId, 1);

            ITicket newTicket = ticketsRepo.Get(projectionId, row, column);
            IProjection projection = projectionsRepo.GetById(projectionId);
            IMovie movie = moviesRepo.GetById(projection.MovieId);
            IRoom room = roomsRepo.GetById(projection.RoomId);
            ICinema cinema = cinemasRepo.GetById(room.CinemaId);

            return new BuyWithoutReservationSummary(new TicketDto
            {
                Id = newTicket.Id,
                ProjectionStartDate = projection.StartDate,
                MovieName = movie.Name,
                CinemaName = cinema.Name,
                RoomNumber = room.Number,
                Row = row,
                Column = column
            });
        }
    }
}