﻿using System;
using System.Threading.Tasks;
using CinemAPI.Data;
using CinemAPI.Models;
using CinemAPI.Models.Contracts.Room;
using CinemAPI.Models.Input.Room;
using System.Web.Http;

namespace CinemAPI.Controllers
{
    public class RoomController : ApiController
    {
        private readonly IRoomRepository roomRepo;

        public RoomController(IRoomRepository roomRepo)
        {
            this.roomRepo = roomRepo;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Index(RoomCreationModel model)
        {
            try
            {
                IRoom room = await roomRepo.GetByCinemaAndNumber(model.CinemaId, model.Number);

                if (room == null)
                {
                    await roomRepo.Insert(new Room(model.Number, model.SeatsPerRow, model.Rows, model.CinemaId));

                    return Ok();
                }

                return BadRequest("Room already exists");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong while creating a room.");
            }
        }
    }
}