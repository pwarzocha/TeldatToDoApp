using System.ComponentModel.DataAnnotations;

namespace TeldatTaskApp.Entities
{
    public class User
    {
        public int UserId { get; set; }
        [MaxLength(20)]
        public string UserName { get; set; }

        public List<UserTask> UserTasks { get; set; }
    }
}
