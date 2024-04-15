
using TeldatTaskApp.Connection;
using TeldatTaskApp.Entities;

namespace TeldatTaskApp.Services
{
    public class UserService : IUserService
    {
        private readonly ToDoAppDbContext _dbContext;
        public UserService(ToDoAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetUserById(int id)
        {
            var user = _dbContext.Users.Where(u => u.UserId == id).ToList().FirstOrDefault();

            return user;
        }
    }
}
