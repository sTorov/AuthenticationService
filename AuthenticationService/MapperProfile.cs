using AutoMapper;

namespace AuthenticationService
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
