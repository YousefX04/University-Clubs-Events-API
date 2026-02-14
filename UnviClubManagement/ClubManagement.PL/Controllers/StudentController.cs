using ClubManagement.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UnviClubManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("Dashboard")]
        public async Task<IActionResult> GetDashboard(int studentId)
        {
            var studentDashboard = await _studentService.GetDashboard(studentId);

            return Ok(studentDashboard);
        }
    }
}
