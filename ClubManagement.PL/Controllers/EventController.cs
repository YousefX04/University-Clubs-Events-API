using ClubManagement.BLL.DTO;
using ClubManagement.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UnviClubManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,ClubLeader")]
        public async Task<IActionResult> AddEvent(AddEventDTO model)
        {
            try
            {
                await _eventService.AddEvent(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,ClubLeader")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            try
            {
                await _eventService.DeleteEvent(eventId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,ClubLeader")]
        public async Task<IActionResult> UpdateEvent(UpdateEventDTO model)
        {
            try
            {
                await _eventService.UpdateEvent(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("{clubId}")]
        public async Task<IActionResult> GetAllEvents(int clubId)
        {
            var events = await _eventService.GetAllEvents(clubId);

            return Ok(events);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEvents();

            return Ok(events);
        }

        [HttpPost("JoinEvent/{eventId}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> JoinEvent(int studentId, int eventId)
        {
            await _eventService.JoinEvent(studentId, eventId);

            return Ok();
        }
    }
}