using ClubManagement.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UnviClubManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ClubLeader")]
    public class ClubLeaderController : ControllerBase
    {
        private readonly IClubLeaderService _clubLeaderService;

        public ClubLeaderController(IClubLeaderService clubLeaderService)
        {
            _clubLeaderService = clubLeaderService;
        }

        [HttpGet("join-requests/club/pending")]
        public async Task<IActionResult> GetPendingJoinClubRequests(int clubLeaderId)
        {
            var pendingMembers = await _clubLeaderService.GetPendingJoinClubRequests(clubLeaderId);

            return Ok(pendingMembers);
        }

        [HttpPut("join-requests/club/accept")]
        public async Task<IActionResult> AcceptJoinClubRequest(int memberId)
        {
            try
            {
                await _clubLeaderService.AcceptJoinClubRequest(memberId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("join-requests/club/reject")]
        public async Task<IActionResult> RejectJoinClubRequest(int memberId)
        {
            try
            {
                await _clubLeaderService.RejectJoinClubRequest(memberId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("club/kick/{clubId}/{memberId}")]
        public async Task<IActionResult> KickClubMember(int memberId)
        {
            try
            {
                await _clubLeaderService.KickClubMember(memberId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("join-requests/event/pending")]
        public async Task<IActionResult> GetPendingJoinEventRequest(int clubLeaderId)
        {
            var pendingMembers = await _clubLeaderService.GetPendingJoinEventRequest(clubLeaderId);

            return Ok(pendingMembers);
        }

        [HttpPut("join-requests/event/accept")]
        public async Task<IActionResult> AcceptJoinEventRequest(int memberId)
        {
            try
            {
                await _clubLeaderService.AcceptJoinEventRequest(memberId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("join-requests/event/reject")]
        public async Task<IActionResult> RejectJoinEventRequest(int memberId)
        {
            try
            {
                await _clubLeaderService.RejectJoinEventRequest(memberId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("event/kick/{eventId}/{memberId}")]
        public async Task<IActionResult> KickEventMember(int memberId, int eventId)
        {
            try
            {
                await _clubLeaderService.KickEventMember(memberId, eventId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet("Dashboard")]
        public async Task<IActionResult> GetDashboard(int clubLeaderId)
        {
            var dashboardData = await _clubLeaderService.GetDashboard(clubLeaderId);

            return Ok(dashboardData);
        }
    }
}
