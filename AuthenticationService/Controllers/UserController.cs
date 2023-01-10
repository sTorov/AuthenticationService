using AuthenticationService.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private ILogger _logger;
        private IUserRepository _userRepository;

        public UserController(ILogger logger, IMapper mapper, IUserRepository userRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;

            logger.WriteEvent("��������� � ������� � ���������");
            logger.WriteError("��������� �� ������ � ���������");
        }

        [HttpGet]
        public User Get()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Login = "ivanov",
                Password = "1111",
                FirstName = "����",
                LastName = "������",
                Email = "ivanov@gmail.com",
                Role = new Role()
                {
                    Id = 1,
                    Name = "������������"
                }
            };
        }

        [Authorize]
        [HttpGet]
        [Route("viewmodel")]
        public UserViewModel GetUserViewModel()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "����",
                LastName = "������",
                Email = "ivan@gmail.com",
                Password = "111",
                Login = "ivanov",
                Role = new Role()
                {
                    Id = 1,
                    Name = "������������"
                }

            };

            var userViewModel = _mapper.Map<UserViewModel>(user);

            return userViewModel;
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<UserViewModel> Authenticate(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                throw new ArgumentNullException("������ �� ���������");

            User? user = _userRepository.GetByLogin(login);
            if (user is null)
                throw new AuthenticationException("������������ �� ������");

            if (user.Password != password)
                throw new AuthenticationException("�������� ������ �� ���������");

            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "AppCookies",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return _mapper.Map<UserViewModel>(user);
        }
    }
}