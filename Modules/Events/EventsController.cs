using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using sk.Core.Exceptions;
using sk.Core.Interfaces;
using sk.Core.Models;
using sk.Core.Models.EventModels;
using sk.Core.Models.UserModels;
using sk.Modules.Services;
using sk.Core.Exceptions;

namespace sk.Modules.Events
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IDataBase<EventModel> _db;
        public EventsController(IDataBase<EventModel> db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEventModel eventModel)
        {
            if(eventModel==null) throw new ArgumentNullException(nameof(eventModel));

            var newEvent = new EventModel
            {
                NameOfEvent = eventModel.NameOfEvent,
                Description = eventModel.Description,

                Location = new Point(eventModel.Longitude, eventModel.Latitude) { SRID = 4326 }
            };

            await _db.Save(newEvent);

            Console.WriteLine("Event saved");

            return Ok(new { eventId = newEvent.Id });
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            bool flag = await _db.IsExist(new EventModel { Id = eventId });

            if (flag)
            {
                await _db.Delete(new EventModel { Id = eventId });
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetEventsList([FromQuery] double? userLon, [FromQuery] double? userLat)
        {
            var eventsList = await _db.GetList();

            var response = eventsList.Select(e => new
            {
                id = e.Id,
                nameOfEvent = e.NameOfEvent,
                description = e.Description,

                location = new { x = e.Location.X, y = e.Location.Y },


                distanceKm = (userLon.HasValue && userLat.HasValue && e.Location != null)
                ? GeolocationService.Distance(e.Location.Y, e.Location.X, userLat.Value, userLon.Value)
                : (double?)null


            }).OrderBy(e => e.distanceKm ?? double.MaxValue).ToList();

            return Ok(response);
        }

        [HttpPost("{eventId}/join")]
        public async Task<IActionResult> JoinEvent(int eventId, [FromQuery] int userId)
        {
            var events = await _db.GetList();
            var targetEvent = events.FirstOrDefault(e => e.Id == eventId);

            if (targetEvent == null) return NotFound("Событие не найдено");

            if (targetEvent.Users == null) targetEvent.Users = new List<User>();

            if (targetEvent.Users.Any(u => u.Id == userId))
            {
                throw new ParticipationException(userId, "Вы уже записаны на это событие");
            }

            targetEvent.Users.Add(new User(userId, "testUser", "pass", "Имя", null, null));

            return Ok(new { message = "Вы успешно записались!", totalParticipants = targetEvent.Users.Count });
        }
    }
}
