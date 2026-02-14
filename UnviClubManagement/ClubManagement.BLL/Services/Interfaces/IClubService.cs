using ClubManagement.BLL.DTO;

namespace ClubManagement.BLL.Services.Interfaces
{
    public interface IClubService
    {
        Task AddClub(AddClubDTO model);
        Task DeleteClub(int id);
        Task UpdateClub(UpdateClubDTO model);
        Task<List<GetAllClubsDTO>> GetAllClubs();
        Task<GetClubDTO> GetClub(int clubleaderid);
        Task JoinClub(int studentId, int clubId);
    }
}
