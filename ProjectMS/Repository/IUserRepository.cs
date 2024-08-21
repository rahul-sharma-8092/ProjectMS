using ProjectMS.Models;

namespace ProjectMS.Repository
{
    public interface IUserRepository
    {
        Users GetUserDetailbyEmailID(string email);
    }
}
