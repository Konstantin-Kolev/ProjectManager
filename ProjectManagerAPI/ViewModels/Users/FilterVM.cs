using Common.Entities;
using System.ComponentModel;
using System.Linq.Expressions;

namespace ProjectManagerAPI.ViewModels.Users
{
    public class FilterVM
    {
        [DisplayName("Username: ")]
        public string Username { get; set; }

        [DisplayName("First Name: ")]
        public string FirstName { get; set; }

        [DisplayName("Last Name: ")]
        public string LastName { get; set; }

        public Expression<Func<User, bool>> GetFilter()
        {
            return i =>
                (string.IsNullOrEmpty(Username) || i.Username.Contains(Username)) &&
                (string.IsNullOrEmpty(FirstName) || i.FirstName.Contains(FirstName)) &&
                (string.IsNullOrEmpty(LastName) || i.LastName.Contains(LastName));

        }
    }
}
