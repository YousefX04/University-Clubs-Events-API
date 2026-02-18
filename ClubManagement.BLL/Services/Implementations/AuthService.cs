using ClubManagement.BLL.DTO;
using ClubManagement.BLL.ExternalServices;
using ClubManagement.BLL.Services.Interfaces;
using ClubManagement.DAL.Data.Models;
using ClubManagement.DAL.Repositories.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace ClubManagement.BLL.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UserLoginDTO> _loginValidator;
        private readonly IValidator<UserRegisterDTO> _registerValidator;
        private readonly UserManager<AppUser> _userManager;
        private readonly JWTService _jwtService;

        public AuthService(IUnitOfWork unitOfWork, IValidator<UserLoginDTO> loginValidator, UserManager<AppUser> userManager, JWTService jwtService, IValidator<UserRegisterDTO> registerValidator)
        {
            _unitOfWork = unitOfWork;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<AuthDTO> Login(UserLoginDTO model)
        {
            var result = _loginValidator.Validate(model);
            if (!result.IsValid)
                throw new Exception(result.ToString(","));

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                throw new Exception("Email Or Password is incorrect");

            var isAuthenticated = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isAuthenticated)
                throw new Exception("Email Or Password is incorrect");

            var userRole = await _userManager.GetRolesAsync(user);

            var token = _jwtService.GenerateToken(
                new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = userRole.First()
                });

            var authDTO = new AuthDTO
            {
                Token = token
            };

            return authDTO;
        }

        public async Task Register(UserRegisterDTO model)
        {
            var result = _registerValidator.Validate(model);
            if (!result.IsValid)
                throw new Exception(result.ToString(","));

            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.Phone,
                EmailConfirmed = true
            };

            var creationResult = await _userManager.CreateAsync(user, model.Password);

            if (!creationResult.Succeeded)
                throw new Exception(string.Join(",", creationResult.Errors.Select(e => e.Description)));

            var roleResult = await _userManager.AddToRoleAsync(user, model.RoleName);

            if (!roleResult.Succeeded)
                throw new Exception(string.Join(",", roleResult.Errors.Select(e => e.Description)));

            var newUser = new User()
            {
                AppUserId = user.Id,
            };

            await _unitOfWork.Users.AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
