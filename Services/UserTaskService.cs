using AutoMapper;

using TeldatTaskApp.Connection;
using TeldatTaskApp.Entities;
using TeldatTaskApp.Models;

namespace TeldatTaskApp.Services

{
    public class UserTaskService : IUserTaskService
    {
        private readonly IMapper _mapper;
        private readonly ToDoAppDbContext _dbContext;
        private readonly IUserService _userService;
        public UserTaskService(IMapper mapper, ToDoAppDbContext dbContext, IUserService userService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _userService = userService;
        }

        public List<UserTask?> GetTasksForDay(DateOnly passedDate)
        {
            List<UserTask?> userTasks = new List<UserTask?>();

            var userTask = _dbContext.Tasks
                .Where(t => t.Date == passedDate)
                .ToList();
            userTasks.AddRange(userTask);


            return userTasks;

        }
        public List<UserTaskViewModel?> UserTaskToViewModel(List<UserTask?> userTasks)
        {
            var userTaskViewModels = new List<UserTaskViewModel?>();
            if (userTasks.Any())
            {
                foreach (var userTask in userTasks)
                {
                    var viewModelUserTask = _mapper.Map<UserTaskViewModel>(userTask);

                    userTaskViewModels.Add(viewModelUserTask);
                }
            };
            return userTaskViewModels;
        }

        public void UpdateUserTask(UserTaskViewModel userTaskViewModel)
        {

            var userTask = _dbContext.Tasks
            .Where(t => t.UserTaskId == userTaskViewModel.UserTaskId)
            .FirstOrDefault();

            if (userTask != null)
            {
                userTask.Name = userTaskViewModel.Name;
                userTask.Description = userTaskViewModel.Description;
                userTask.Date = userTaskViewModel.Date;
            }

            _dbContext.SaveChanges();
        }


        public void DeleteUserTask(int userTaskId)
        {

            var userTask = GetUserTaskById(userTaskId);
            if (userTask != null)
            {
                _dbContext.Remove(userTask);
            }
            else
            {
                throw new InvalidOperationException("Object with id: ${userTaskId} can't be null for further removal to process.");
            }

            _dbContext.SaveChanges();


        }

        public UserTask GetUserTaskById(int userTaskId)
        {
            var userTask = _dbContext.Tasks
            .Where(t => t.UserTaskId == userTaskId)
            .FirstOrDefault();
            if (userTask != null)
            {
                return userTask;
            }
            else
            {
                throw new InvalidOperationException("Object with id: ${userTaskId} can't be null for further getting user by Id to process.");
            }
        }

        public void AddUserTask(UserTaskViewModel userTaskViewModel)
        {
            var userTask = _mapper.Map<UserTask>(userTaskViewModel);
            userTask.User = _userService.GetUserById(1);
            _dbContext.Add(userTask);

            _dbContext.SaveChanges();

        }
    }
}
