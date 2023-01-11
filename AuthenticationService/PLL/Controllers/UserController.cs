using AuthenticationService.BLL.Models;
using AuthenticationService.DAL.Repositories;
using AuthenticationService.BLL.Exceptions;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Security.Claims;

namespace AuthenticationService.PLL.Controllers
{
    [ExceptionHandler]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private IUserRepository _userRepository;

        public UserController(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
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

        [Authorize(Roles = "�������������, admin")]
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

        [HttpGet]
        [Route("rus")]
        [Authorize(Policy = "FromRussia")]
        public string FromRussia()
        {
            var login = HttpContext.User.FindFirst(ClaimsIdentity.DefaultNameClaimType);
            var role = HttpContext.User.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
            return $"�����: {login.Value}\n����: {role.Value}\n������ ��� ������� ��";
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
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name),
                new Claim("fromRussia", user.Email.Contains(".ru").ToString())
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