using DeliverIt13.Data.Enums;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Services.Models
{
	public class UserAuthDTO
	{
		public UserAuthDTO(User user)
        {
            this.UserId = user.UserId;
            this.Type = user.Type;
            this.Email = user.Email;
        }

        public int UserId { get; set; }
		public string Email { get; }
        public UserType Type { get; set; }
	}
}
