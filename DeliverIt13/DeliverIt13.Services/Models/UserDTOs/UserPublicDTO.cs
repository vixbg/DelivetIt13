using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Services.Models
{
    public class UserPublicDTO
    {
        public UserPublicDTO(User user)
        {
            this.Type = user.Type;
            this.Password = user.Password;
            this.Email = user.Email;
        }
        public string Email { get; set; }

        public UserType Type { get; set; }

        public string Password { get; set; }

    }

}