namespace AuthenticationService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users;

        public UserRepository()
        {
            _users = new List<User>()
            {
                new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Петя",
                    LastName = "Петров",
                    Login = "petya",
                    Password = "2222",
                    Email = "petya@gmail.ru",
                    Role = new Role()
                    {
                        Id = 2,
                        Name = "Администратор"
                    }
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Login = "ivanov",
                    Password = "1111",
                    Email = "ivanov@gmail.com",
                    Role = new Role()
                    {
                        Id = 1,
                        Name = "Пользователь"
                    }
                },
                new User()
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Вася",
                    LastName = "Сидоров",
                    Login = "vasya",
                    Password = "3333",
                    Email = "vasya@gmail.ru",
                    Role = new Role()
                    {
                        Id = 1,
                        Name = "Пользователь"
                    }
                },
            };
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User? GetByLogin(string login)
        {
            return GetAll().FirstOrDefault(user => user.Login == login);
        }
    }
}
