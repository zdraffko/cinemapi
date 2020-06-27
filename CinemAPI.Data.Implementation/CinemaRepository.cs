﻿using CinemAPI.Data.EF;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Cinema;
using System.Linq;

namespace CinemAPI.Data.Implementation
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly CinemaDbContext db;

        public CinemaRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public ICinema GetById(int cinemaId)
        {
            return db.Cinemas.FirstOrDefault(c => c.Id == cinemaId);
        }

        public ICinema GetByNameAndAddress(string name, string address)
        {
            return db.Cinemas.Where(x => x.Name == name &&
                                         x.Address == address)
                             .FirstOrDefault();
        }

        public void Insert(ICinemaCreation cinema)
        {
            Cinema newCinema = new Cinema(cinema.Name, cinema.Address);

            db.Cinemas.Add(newCinema);

            db.SaveChanges();
        }
    }
}