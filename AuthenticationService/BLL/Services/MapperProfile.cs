using AuthenticationService.BLL.Models;
using AutoMapper;

namespace AuthenticationService.BLL.Services
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserViewModel>()
                .ConstructUsing(user => new UserViewModel(user));
        }
    }
}
