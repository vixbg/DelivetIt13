using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Services.Models.UserDTOs
{
    public class UserCreatePublicDTO
    {
        public UserCreatePublicDTO()
        {
            
        }
        public UserCreatePublicDTO(User user)
        {
            this.Type = UserType.Customer;
            this.Password = user.Password;
            this.Email = user.Email;
        }

        public string Email { get; set; }
        private UserType Type { get; set; }
        public string Password { get; set; }
    }
}