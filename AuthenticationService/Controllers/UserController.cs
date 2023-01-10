using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private ILogger _logger;

        public UserController(ILogger logger)
        {
            _logger = logger;

            logger.WriteEvent("��������� � ������� � ���������");
            logger.WriteError("��������� �� ������ � ���������");
        }

        [HttpGet]
        public User Get()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Login = "Ivanov",
                Password = "1111",
                FirstName = "����",
                LastName = "������",
                Email = "ivanov@gmail.com"
            };
        }
    }
}