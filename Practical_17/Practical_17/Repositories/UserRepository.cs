using Microsoft.EntityFrameworkCore;
using Practical_17.Data;
using Practical_17.Enums;
using Practical_17.Interfaces;
using Practical_17.Models;
using Practical_17.ViewDataModel;

namespace Practical_17.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _db;

        public UserRepository(DatabaseContext db)
        {
            _db = db;
        }

        public async Task RegisterUserAsync(RegisterViewModel user)
        {
            _db.Users.Add(ChangeViewModelToModel(user));
            await _db.SaveChangesAsync();
            User temp = await _db.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (temp != null)
            {
                _db.UserRoles.Add(new UserRole() { RoleId = 2, UserId = temp.Id });
                await _db.SaveChangesAsync();
            }

        }

        public async Task<UserLoginStatus> LoginUserAsync(LoginViewModel model)
        {
            if (model == null)
            {
                return UserLoginStatus.UserNull;
            }

            User user = await _db.Users.FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
            {
                return UserLoginStatus.UserNotFound;
            }

            if (user.Password != model.Password)
            {
                return UserLoginStatus.InvalidPassword;
            }
            else
            {
                return UserLoginStatus.LoginSuccess;
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public List<string> GetUserRoles(int id)
        {
            var roles = _db.UserRoles
                .Where(x => x.UserId == id)
                .Join(_db.Roles, x => x.RoleId, x => x.Id, (user, role) => new { user.UserId, role.RoleName })
                .Select(x => x.RoleName);

            return roles.ToList();
        }

        private User ChangeViewModelToModel(RegisterViewModel model)
        {
            User user = new User()
            {
                Id = model.Id,
                Email = model.Email,
                Password = model.Password,
                FirstName = model.FirstName,
                LastName = model.LastName,
                MobileNumber = model.MobileNumber,
            };



            return user;
        }

    }
}

