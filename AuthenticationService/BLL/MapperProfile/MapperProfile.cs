using AuthenticationService.BLL.Models;
using AutoMapper;

namespace AuthenticationService.BLL.MapperProfile
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
