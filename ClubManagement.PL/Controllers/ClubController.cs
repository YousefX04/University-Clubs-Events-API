using ClubManagement.BLL.DTO;
using ClubManagement.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UnviClubManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }


        [HttpPost]
        [Authorize(Roles = "Admin,ClubLeader")]
        public async Task<IActionResult> AddClub(AddClubDTO model)
        {
            try
            {
                await _clubService.AddClub(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,ClubLeader")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            try
            {
                await _clubService.DeleteClub(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin,ClubLeader")]
        public async Task<IActionResult> UpdateClub(UpdateClubDTO model)
        {
            try
            {
                await _clubService.UpdateClub(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClubs()
        {
            var clubs = await _clubService.GetAllClubs();

            return Ok(clubs);
        }

        [HttpGet("{clubLeaderId}")]
        public async Task<IActionResult> GetClub(int clubLeaderId)
        {
            try
            {
                var club = await _clubService.GetClub(clubLeaderId);
                return Ok(club);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("JoinClub/{clubId}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> JoinClub(int studentId, int clubId)
        {
            await _clubService.JoinClub(studentId, clubId);

            return Ok();
        }
    }
}
