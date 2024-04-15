using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using TeldatTaskApp.Models;
using TeldatTaskApp.Services;

namespace TeldatTaskApp.Controllers
{
    public class ToDoAppController : Controller
    {
        private IUserTaskService _userTaskService;
        private IUserService _userService;
        public ToDoAppController(IUserTaskService userTaskService, IUserService userService)
        {
            _userTaskService = userTaskService;
            _userService = userService;
        }

        [Route("")]
        [Route("{controller}/index")]
        public IActionResult Index()
        {
            ViewBag.User = _userService.GetUserById(1).UserName;
            ViewBag.Date = DateTime.Today.ToString("yyyy-MM-dd");
            
            return View();
        }

        [HttpPost]
        [Route("{controller}/tasks/{passedDate}")]
        public List<UserTaskViewModel?> GetUserTasksForDate([FromRoute(Name = "passedDate")]string passedDate)
        {

            DateTime date = DateTime.ParseExact(passedDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var dateOnly = DateOnly.FromDateTime(date);

            var userTasks = _userTaskService.GetTasksForDay(dateOnly);
            var viewModels = _userTaskService.UserTaskToViewModel(userTasks);

            return viewModels;
        }

        [HttpPatch]
        [Route("{controller}/tasks")]
        public ActionResult UpdateUserTask(UserTaskViewModel userTaskViewModel)
        {
            try
            {
                _userTaskService.UpdateUserTask(userTaskViewModel);


                return Ok();
            }
            catch (Exception exexception)
            {
                return BadRequest(exexception.Message);
            }

        }

        [HttpDelete]
        [Route("{controller}/tasks/{userTaskId}")]
        public ActionResult DeleteUserTask([FromRoute(Name = "userTaskId")] int userTaskId)
        {
            try
            {
                _userTaskService.DeleteUserTask(userTaskId);
                return Ok();
            }
            catch (Exception exexception)
            {
                return BadRequest(exexception.Message);
            }

        }

        [HttpPost]
        [Route("{controller}/tasks")]
        public ActionResult AddUserTask(UserTaskViewModel userTaskViewModel)
        {
            try
            {
                _userTaskService.AddUserTask(userTaskViewModel);


                return Ok();
            }
            catch (Exception exexception)
            {
                return BadRequest(exexception.Message);
            }


        }
    }
}
