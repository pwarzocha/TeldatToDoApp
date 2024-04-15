using TeldatTaskApp.Connection;
using TeldatTaskApp.Models.Mapper;
using TeldatTaskApp.Services;
namespace TeldatTaskApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(typeof(UserTaskMapper));
            builder.Services.AddTransient<IUserTaskService, UserTaskService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddDbContext<ToDoAppDbContext>();

            PrototypeStartupConfig.EnsureDbCreated();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
