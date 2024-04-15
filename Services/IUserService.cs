using TeldatTaskApp.Entities;

namespace TeldatTaskApp.Services
{
    public interface IUserService
    {
        User GetUserById(int id);
    }
}