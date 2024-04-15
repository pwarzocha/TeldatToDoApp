using System.ComponentModel.DataAnnotations;

namespace TeldatTaskApp.Entities
{
    public class UserTask
    {

        public int? UserTaskId { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string? Description { get; set; }
        public DateOnly? Date { get; set; }
        public DateTime? LastChange {  get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        
    }
}
