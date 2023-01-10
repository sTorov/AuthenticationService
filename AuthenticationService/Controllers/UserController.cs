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

            logger.WriteEvent("Сообщение о событии в программе");
            logger.WriteError("Сообщение об ошибке в программе");
        }

        [HttpGet]
        public User Get()
        {
            return new User
            {
                Id = Guid.NewGuid(),
                Login = "Ivanov",
                Password = "1111",
                FirstName = "Иван",
                LastName = "Иванов",
                Email = "ivanov@gmail.com"
            };
        }
    }
}