using System.Net.Mail;

namespace AuthenticationService.BLL.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public bool FromRussia { get; set; }

        public UserViewModel(User user)
        {
            Id = user.Id;
            FullName = GetFullName(user.FirstName, user.LastName);
            FromRussia = GetFromRussia(user.Email);
        }

        private string GetFullName(string jirstName, string lastName) =>
            string.Concat(jirstName, " ", lastName);

        private bool GetFromRussia(string email)
        {
            MailAddress mailAddress = new MailAddress(email);

            if (mailAddress.Host.Contains(".ru"))
                return true;
            return false;
        }
    }
}
