using ClubManagement.BLL.DTO;

namespace ClubManagement.BLL.Services.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDashboardDTO> GetDashboard(int studentId);
    }
}
