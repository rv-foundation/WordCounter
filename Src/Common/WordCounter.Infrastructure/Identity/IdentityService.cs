using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WordCounter.Application.Common.Exceptions;
using WordCounter.Application.Common.Interfaces;
using WordCounter.Application.Common.Models;
using WordCounter.Application.Dto;

namespace WordCounter.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public IdentityService(UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        public async Task<string> GetUserNameAsync(string userId) {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new UnauthorizeException();
            }

            return user.UserName;
        }

        public async Task<ApplicationUserDto> CheckUserPassword(string email, string password) {
            ApplicationUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return new ApplicationUserDto { Email = user.Email, Id = user.Id, UserName = user.UserName };
            }

            return null;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password) {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> UserIsInRole(string userId, string role) {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<Result> DeleteUserAsync(string userId) {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user) {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
    }
}
