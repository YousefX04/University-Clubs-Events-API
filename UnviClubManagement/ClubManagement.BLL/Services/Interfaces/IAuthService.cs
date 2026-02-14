using ClubManagement.BLL.DTO;

namespace ClubManagement.BLL.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthDTO> Login(UserLoginDTO model);
        Task Register(UserRegisterDTO model);
    }
}
