using TeldatTaskApp.Connection;
using TeldatTaskApp.Entities;

namespace TeldatTaskApp
{
    public class PrototypeStartupConfig
    {
        public static void EnsureDbCreated()
        {
            using (var context = new ToDoAppDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var initUser = new User() {UserName = "DefaultUser" };

                var initTask1 = new UserTask() { Name = "TaskNo1", Description = "Desc1", User = initUser, Date = new DateOnly(2024, 4, 12), LastChange = DateTime.Now };
                var initTask2 = new UserTask() { Name = "TaskNo2", Description = "Desc2", User = initUser, Date = DateOnly.FromDateTime(DateTime.Now), LastChange = DateTime.Now };
                var initTask3 = new UserTask() { Name = "TaskNo3", User = initUser, Date = new DateOnly(2024, 3, 13), LastChange = DateTime.Now };


                context.Users.Add(initUser);
                context.Tasks.Add(initTask1);
                context.Tasks.Add(initTask2);
                context.Tasks.Add(initTask3);
                

                context.SaveChanges();

            }
        }
    }
}
