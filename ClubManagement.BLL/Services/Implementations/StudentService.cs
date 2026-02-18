using ClubManagement.BLL.DTO;
using ClubManagement.BLL.Services.Interfaces;
using ClubManagement.DAL.Enums;
using ClubManagement.DAL.Repositories.Interfaces;

namespace ClubManagement.BLL.Services.Implementations
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<StudentDashboardDTO> GetDashboard(int studentId)
        {
            var numberOfJoinedClubs = await _unitOfWork.ClubMembers
                .CountAsync(cm => cm.UserID == studentId && cm.Status == Status.Accepted.ToString());

            var numberOfJoinedEvents = await _unitOfWork.EventMembers
                .CountAsync(em => em.UserID == studentId && em.Status == Status.Accepted.ToString());

            var clubList = await _unitOfWork.ClubMembers
                .GetAllAsync(
                filter: cm => cm.UserID == studentId && cm.Status == Status.Accepted.ToString(),
                selector: cm => new ClubJoinedDTO
                {
                    ClubName = cm.Club.ClubName,
                    Desc = cm.Club.Description
                });

            var eventList = await _unitOfWork.EventMembers
                .GetAllAsync(
                filter: em => em.UserID == studentId && em.Status == Status.Accepted.ToString(),
                selector: cm => new EventJoinedDTO
                {
                    EventName = cm.Event.EventName,
                    Desc = cm.Event.Description,
                    ClubName = cm.Event.Club.ClubName,
                    StartAt = cm.Event.StartAt,
                    EndAt = cm.Event.EndAt
                });

            var studentDashboard = new StudentDashboardDTO
            {
                NumOfJoinedClubs = numberOfJoinedClubs,
                NumOfJoinedEvents = numberOfJoinedEvents,
                JoinedClubs = clubList,
                JoinedEvents = eventList
            };

            return studentDashboard;
        }
    }
}
