using ClubManagement.BLL.DTO;
using ClubManagement.BLL.Services.Interfaces;
using ClubManagement.DAL.Enums;
using ClubManagement.DAL.Repositories.Interfaces;

namespace ClubManagement.BLL.Services.Implementations
{
    public class ClubLeaderService : IClubLeaderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ClubLeaderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AcceptJoinClubRequest(int memberId)
        {
            var member = await _unitOfWork.Users.GetAsync(m => m.Id == memberId);

            if (member == null)
                throw new Exception("Student Id Does Not Exist");

            var cMember = await _unitOfWork.ClubMembers.GetAsync(m => m.UserID == memberId);

            if (cMember == null || cMember.Status != Status.Pending.ToString())
                throw new Exception("Student id does not exist or its not pending");

            cMember.Status = Status.Accepted.ToString();

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task AcceptJoinEventRequest(int memberId)
        {
            var member = await _unitOfWork.Users.GetAsync(m => m.Id == memberId);

            if (member == null)
                throw new Exception("Student Id does not exist");

            var eMember = await _unitOfWork.EventMembers.GetAsync(m => m.UserID == memberId);

            if (eMember == null || eMember.Status != Status.Pending.ToString())
                throw new Exception("id does not exist or its not pending");

            eMember.Status = Status.Accepted.ToString();

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ClubLeaderDashboardDTO> GetDashboard(int clubLeaderId)
        {
            var clubName = await _unitOfWork.Clubs.GetAsync(
                filter: c => c.ClubLeaderID == clubLeaderId,
                selector: c => c.ClubName
                );


            var clubStatus = await _unitOfWork.Clubs.GetAsync(
                filter: c => c.ClubLeaderID == clubLeaderId,
                selector: c => c.Status
                );

            var pendingClubMembersCount = await _unitOfWork.ClubMembers
                .CountAsync(cm => cm.Club.ClubLeaderID == clubLeaderId && cm.Status == Status.Pending.ToString());

            var acceptedClubMembersCount = await _unitOfWork.ClubMembers
                .CountAsync(cm => cm.Club.ClubLeaderID == clubLeaderId && cm.Status == Status.Accepted.ToString());

            var rejectedClubMembersCount = await _unitOfWork.ClubMembers
                .CountAsync(cm => cm.Club.ClubLeaderID == clubLeaderId && cm.Status == Status.Rejected.ToString());

            var totalClubMembersCount = await _unitOfWork.ClubMembers
                .CountAsync(cm => cm.Club.ClubLeaderID == clubLeaderId);

            var pendingEventMembersCount = await _unitOfWork.EventMembers
                .CountAsync(em => em.Event.Club.ClubLeaderID == clubLeaderId && em.Status == Status.Pending.ToString());

            var acceptedEventMembersCount = await _unitOfWork.EventMembers
                .CountAsync(em => em.Event.Club.ClubLeaderID == clubLeaderId && em.Status == Status.Accepted.ToString());

            var rejectedEventMembersCount = await _unitOfWork.EventMembers
                .CountAsync(em => em.Event.Club.ClubLeaderID == clubLeaderId && em.Status == Status.Rejected.ToString());

            var acceptedEventsCount = await _unitOfWork.Events
                .CountAsync(e => e.Club.ClubLeaderID == clubLeaderId && e.Status == Status.Accepted.ToString());

            var dashboardData = new ClubLeaderDashboardDTO
            {
                ClubName = clubName,
                ClubStatus = clubStatus,
                PendingClubMembersCount = pendingClubMembersCount,
                AcceptedClubMembersCount = acceptedClubMembersCount,
                RejectedClubMembersCount = rejectedClubMembersCount,
                TotalClubMembersCount = totalClubMembersCount,
                PendingEventMembersCount = pendingEventMembersCount,
                AcceptedEventMembersCount = acceptedEventMembersCount,
                RejectedEventMembersCount = rejectedEventMembersCount,
                AcceptedEventsCount = acceptedEventsCount,
            };

            return dashboardData;
        }

        public async Task<List<PendingClubJoinRequestDTO>> GetPendingJoinClubRequests(int clubLeaderId)
        {
            var pendingMembers = await _unitOfWork.ClubMembers
                .GetAllAsync(
                filter: cm => cm.Status == Status.Pending.ToString() && cm.Club.ClubLeaderID == clubLeaderId,
                selector: cm => new PendingClubJoinRequestDTO
                {
                    Id = cm.User.Id,
                    StudentName = cm.User.AppUser.UserName,
                });

            return pendingMembers;
        }

        public async Task<List<PendingEventJoinRequestDTO>> GetPendingJoinEventRequest(int clubLeaderId)
        {
            var pendingMembers = await _unitOfWork.EventMembers
                .GetAllAsync(
                filter: em => em.Status == Status.Pending.ToString() && em.Event.Club.ClubLeaderID == clubLeaderId,
                selector: em => new PendingEventJoinRequestDTO
                {
                    Id = em.User.Id,
                    StudentName = em.User.AppUser.UserName,
                });

            return pendingMembers;
        }

        public async Task KickClubMember(int memberId)
        {
            var member = await _unitOfWork.Users.GetAsync(m => m.Id == memberId);

            if (member == null)
                throw new Exception("Student Id does not exist");

            var cMember = await _unitOfWork.ClubMembers.GetAsync(m => m.UserID == memberId);

            if (cMember == null || cMember.Status != Status.Accepted.ToString())
                throw new Exception("Student Id does not exist or its not Accepted");

            await _unitOfWork.EventMembers.DeleteWhereAsync(em => em.UserID == memberId);

            _unitOfWork.ClubMembers.Delete(cMember);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task KickEventMember(int memberId, int eventId)
        {
            var member = await _unitOfWork.Users.GetAsync(m => m.Id == memberId);

            if (member == null)
                throw new Exception("Student Id does not exist");

            var eMember = await _unitOfWork.EventMembers.GetAsync(m => m.UserID == memberId && m.EventID == eventId);

            if (eMember == null || eMember.Status != Status.Accepted.ToString())
                throw new Exception("Student Id does not exist or its not Accepted");

            _unitOfWork.EventMembers.Delete(eMember);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RejectJoinClubRequest(int memberId)
        {
            var member = await _unitOfWork.Users.GetAsync(m => m.Id == memberId);

            if (member == null)
                throw new Exception("Student Id does not exist");

            var cMember = await _unitOfWork.ClubMembers.GetAsync(m => m.UserID == memberId);

            if (cMember == null || cMember.Status != Status.Pending.ToString())
                throw new Exception("Student Id does not exist or its not pending");

            cMember.Status = Status.Rejected.ToString();

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RejectJoinEventRequest(int memberId)
        {
            var member = await _unitOfWork.Users.GetAsync(m => m.Id == memberId);

            if (member == null)
                throw new Exception("Student Id does not exist");

            var eMember = await _unitOfWork.EventMembers.GetAsync(m => m.UserID == memberId);

            if (eMember == null || eMember.Status != Status.Pending.ToString())
                throw new Exception("Student Id does not exist or its not pending");

            eMember.Status = Status.Rejected.ToString();

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
