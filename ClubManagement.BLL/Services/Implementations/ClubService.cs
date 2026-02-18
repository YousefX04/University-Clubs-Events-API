using ClubManagement.BLL.DTO;
using ClubManagement.BLL.Services.Interfaces;
using ClubManagement.DAL.Data.Models;
using ClubManagement.DAL.Enums;
using ClubManagement.DAL.Repositories.Interfaces;
using FluentValidation;

namespace ClubManagement.BLL.Services.Implementations
{
    public class ClubService : IClubService
    {
        private readonly IUnitOfWork _uniUnitOfWork;
        private readonly IValidator<AddClubDTO> _addValidator;
        private readonly IValidator<UpdateClubDTO> _updateValidator;


        public ClubService(IUnitOfWork uniUnitOfWork, IValidator<AddClubDTO> addValidator, IValidator<UpdateClubDTO> updateValidator)
        {
            _uniUnitOfWork = uniUnitOfWork;
            _addValidator = addValidator;
            _updateValidator = updateValidator;
        }

        public async Task AddClub(AddClubDTO model)
        {
            var result = _addValidator.Validate(model);
            if (!result.IsValid)
                throw new Exception(result.ToString(","));

            var clubName = await _uniUnitOfWork.Clubs.GetAsync(c => c.ClubName == model.ClubName);

            if (clubName != null)
                throw new Exception("Name Already Exist");


            var user = await _uniUnitOfWork.Users.GetAsync(u => u.Id == model.ClubLeaderID);

            if (user == null)
                throw new Exception("User Id does not exist");


            var club = await _uniUnitOfWork.Clubs.GetAsync(c => c.ClubLeaderID == model.ClubLeaderID);

            if (club != null)
                throw new Exception("You Already have a club");


            var newClub = new Club()
            {
                ClubName = model.ClubName,
                Description = model.Desc,
                ClubLeaderID = model.ClubLeaderID,
                Status = Status.Pending.ToString()
            };

            await _uniUnitOfWork.Clubs.AddAsync(newClub);
            await _uniUnitOfWork.SaveChangesAsync();
        }

        public async Task DeleteClub(int id)
        {
            var club = await _uniUnitOfWork.Clubs.GetAsync(c => c.Id == id);

            if (club == null)
                throw new Exception("id does not exist");


            await _uniUnitOfWork.ClubMembers.DeleteWhereAsync(cm => cm.ClubID == id);

            _uniUnitOfWork.Clubs.Delete(club);

            await _uniUnitOfWork.SaveChangesAsync();
        }

        public async Task<List<GetAllClubsDTO>> GetAllClubs()
        {
            var clubs = await _uniUnitOfWork.Clubs
                .GetAllAsync(
                filter: c => c.Status == Status.Accepted.ToString(),
                selector: c => new GetAllClubsDTO
                {
                    Id = c.Id,
                    ClubName = c.ClubName,
                    Description = c.Description,
                    ClubLeaderName = c.User.AppUser.UserName
                });

            return clubs;
        }

        public async Task<GetClubDTO> GetClub(int clubleaderid)
        {
            var club = await _uniUnitOfWork.Clubs
                .GetAsync(
                filter: c => c.ClubLeaderID == clubleaderid && c.Status == Status.Accepted.ToString(),
                selector: c => new GetClubDTO
                {
                    Id = c.Id,
                    ClubName = c.ClubName,
                    Description = c.Description,
                    ClubLeaderName = c.User.AppUser.UserName,
                    Members = c.Members.Select(m => new GetClubMemeberDTO
                    {
                        Id = m.UserID,
                        Name = m.User.AppUser.UserName,
                        Status = m.Status
                    }).ToList()
                });

            if (club == null)
                throw new Exception("id does not exist");

            return club;
        }

        public async Task JoinClub(int studentId, int clubId)
        {
            await _uniUnitOfWork.ClubMembers.AddAsync(new ClubMember
            {
                ClubID = clubId,
                UserID = studentId,
                Status = Status.Pending.ToString()
            });

            await _uniUnitOfWork.SaveChangesAsync();
        }

        public async Task UpdateClub(UpdateClubDTO model)
        {
            var result = _updateValidator.Validate(model);
            if (!result.IsValid)
                throw new Exception(result.ToString(","));

            var club = await _uniUnitOfWork.Clubs.GetAsync(c => c.Id == model.ClubID);

            if (club == null)
                throw new Exception("ClubID Does not exist");

            var clubName = await _uniUnitOfWork.Clubs.GetAsync(c => c.ClubName == model.ClubName);

            if (clubName != null)
                throw new Exception("Name Already Exist");


            var updatedClub = new ClubUpdate
            {
                Id = model.ClubID,
                OldName = club.ClubName,
                NewName = model.ClubName,
                OldDescription = club.Description,
                NewDescription = model.Desc,
            };

            await _uniUnitOfWork.ClubUpdates.AddAsync(updatedClub);

            await _uniUnitOfWork.SaveChangesAsync();
        }
    }
}
