
using TeldatTaskApp.Entities;
using TeldatTaskApp.Models;

namespace TeldatTaskApp.Services
{
    public interface IUserTaskService
    {
        List<UserTask?> GetTasksForDay(DateOnly passedDate);
        List<UserTaskViewModel?> UserTaskToViewModel(List<UserTask?> userTasks);
        void UpdateUserTask(UserTaskViewModel userTaskViewModel);
        void DeleteUserTask(int userTaskId);
        void AddUserTask(UserTaskViewModel userTaskViewModel);
    }
}
