using ProjectMS.Models;

namespace ProjectMS.Service
{
    public interface IUserService
    {
        Users GetUserDetailbyEmailID(string email);
    }
}
