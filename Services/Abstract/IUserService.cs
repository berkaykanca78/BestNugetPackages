using BaseWebApp.Models;

namespace BaseWebApp.Services.Abstract
{
    public interface IUserService
    {
        List<User> GetUsers();
        Task<List<User>> GetUsersAsync();
        User GetUserByUserName(string userName);
        User AddUser(User user);
    }
}
