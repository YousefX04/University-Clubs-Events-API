using ClubManagement.BLL.DTO;

namespace ClubManagement.BLL.Services.Interfaces
{
    public interface IClubLeaderService
    {
        Task<List<PendingClubJoinRequestDTO>> GetPendingJoinClubRequests(int clubLeaderId);
        Task AcceptJoinClubRequest(int memberId);
        Task RejectJoinClubRequest(int memberId);
        Task KickClubMember(int memberId);
        Task<List<PendingEventJoinRequestDTO>> GetPendingJoinEventRequest(int clubLeaderId);
        Task AcceptJoinEventRequest(int memberId);
        Task RejectJoinEventRequest(int memberId);
        Task KickEventMember(int memberId, int eventId);
        Task<ClubLeaderDashboardDTO> GetDashboard(int clubLeaderId);
    }
}
