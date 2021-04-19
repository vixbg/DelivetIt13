using DeliverIt13.Services.Models;
using DeliverIt13.Data.Models;

namespace DeliverIt13.Web.Helpers
{
    public interface IAuthHelper
    {
        UserAuthDTO TryGetUser(string credentialsHeader);
    }
}