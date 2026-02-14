using ClubManagement.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UnviClubManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("PendingClubs")]
        public async Task<IActionResult> GetAllPendingClubs()
        {
            var clubs = await _adminService.GetAllPendingClubs();

            return Ok(clubs);
        }

        [HttpPut("AcceptClubRequest")]
        public async Task<IActionResult> AcceptStatusClub(int clubId)
        {
            try
            {
                await _adminService.AcceptStatusClub(clubId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("RejectClubRequest")]
        public async Task<IActionResult> RejectStatusClub(int clubId)
        {
            try
            {
                await _adminService.RejectStatusClub(clubId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("PendingUpdatedClubs")]
        public async Task<IActionResult> GetAllPedningUpdatedClubs()
        {
            var clubs = await _adminService.GetAllPedningUpdatedClubs();

            return Ok(clubs);
        }

        [HttpPut("AcceptUpdatedClubsRequest")]
        public async Task<IActionResult> AcceptUpdatedClub(int clubId)
        {
            try
            {
                await _adminService.AcceptUpdatedClub(clubId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("RejectUpdatedClubsRequest")]
        public async Task<IActionResult> RejectUpdatedClub(int clubId)
        {
            try
            {
                await _adminService.RejectUpdatedClub(clubId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("PendingEvents")]
        public async Task<IActionResult> GetAllPendingEvents()
        {
            var events = await _adminService.GetAllPendingEvents();

            return Ok(events);
        }

        [HttpPut("AcceptEventRequest")]
        public async Task<IActionResult> AcceptStatusEvent(int eventId)
        {
            try
            {
                await _adminService.AcceptStatusEvent(eventId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("RejectEventRequest")]
        public async Task<IActionResult> RejectStatusEvent(int eventId)
        {
            try
            {
                await _adminService.RejectStatusEvent(eventId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("PendingUpdatedEvents")]
        public async Task<IActionResult> GetAllPedningUpdatedEvents()
        {
            var Event = await _adminService.GetAllPedningUpdatedEvents();

            return Ok(Event);
        }

        [HttpPut("AcceptUpdatedEventRequest")]
        public async Task<IActionResult> AcceptUpdatedEvent(int eventId)
        {
            try
            {
                await _adminService.AcceptUpdatedEvent(eventId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("RejectedUpdatedEventRequest")]
        public async Task<IActionResult> RejectUpdatedEvent(int eventId)
        {
            try
            {
                await _adminService.RejectUpdatedEvent(eventId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("Dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var dashboard = await _adminService.GetDashboard();

            return Ok(dashboard);
        }
    }
}
