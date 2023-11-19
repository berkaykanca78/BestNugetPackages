using BaseWebApp.Models;
using BaseWebApp.Services.Abstract;
using BaseWebApp.Utilities.EFCore;
using Microsoft.EntityFrameworkCore;

namespace BaseWebApp.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly MyDbContext _context;

        public UserService(MyDbContext context)
        {
            _context = context;
        }
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public Task<List<User>> GetUsersAsync()
        {
            return _context.Users.ToListAsync();
        }

        public User GetUserByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(c => c.UserName == userName);
        }

        public User AddUser(User user)
        {
           _context.Users.AddAsync(user);
           _context.SaveChangesAsync();
           return user;
        }
    }
}
