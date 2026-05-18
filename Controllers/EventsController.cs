using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sk.Services;

namespace sk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IDBService _dBService;
        public EventsController(IDBService dBService)
        {
            _dBService = dBService;
        }

        //[Authorize(Roles = "Investigator, Admin")]
        [HttpPost("create")]
        public IActionResult EventCreate()
        {
            return Ok();
        }

        //[Authorize(Roles = "Investigator, Admin")]
        [HttpDelete("delete/{id}")]
        public IActionResult EventDelete()
        {
            return Ok();
        }

        [HttpGet]
        //[Authorize(Roles = "User, Investigator, Admin")]
        public IActionResult GetAllEvents()
        {
            var events = _dBService.GetAllEvents();
            return Ok(events);
        }

        [HttpPost("{id}/subscriber")]
        //[Authorize(Roles = "User")]
        public IActionResult SubscribeToEvents(int id)
        {
            var username = User.Identity.Name ?? "Unknown";

            var success = _dBService.SubscribeUser(id, username);

            if (!success) return NotFound("Событие не найдено");

            return Ok(new {Message = $"Пользователь {username} записан"});
        }
    }
}
