using Practical_17.Enums;
using Practical_17.Models;
using Practical_17.ViewDataModel;

namespace Practical_17.Interfaces
{
    public interface IUserRepository
    {
        Task RegisterUserAsync(RegisterViewModel user);

        Task<UserLoginStatus> LoginUserAsync(LoginViewModel model);

        Task<User> GetUserByEmailAsync(string email);

        List<string> GetUserRoles(int id);
    }
}
