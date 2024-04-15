using AutoMapper;
using TeldatTaskApp.Entities;

namespace TeldatTaskApp.Models.Mapper
{
    public class UserTaskMapper : Profile
    {
        public UserTaskMapper() {
            CreateMap<UserTask, UserTaskViewModel>();
            CreateMap<UserTaskViewModel, UserTask>();
        }
    }
}
