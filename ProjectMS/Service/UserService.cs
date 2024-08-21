using ProjectMS.Models;
using ProjectMS.Repository;

namespace ProjectMS.Service
{
    public class UserService : IUserService
    {
        #region Constructor
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        #endregion

        public Users GetUserDetailbyEmailID(string email)
        {
            return userRepository.GetUserDetailbyEmailID(email);
        }
    }
}
