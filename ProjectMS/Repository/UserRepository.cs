using ProjectMS.Models;
using ProjectMS.DBHandler;

namespace ProjectMS.Repository
{
    public class UserRepository : IUser
    {
        private readonly IConfiguration configuration;
        private readonly UserHandler userHandler;

        public UserRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
            userHandler = new UserHandler(configuration);
        }

        bool IUser.CheckEmailExists(string email)
        {
            return userHandler.CheckEmailExists(email);
        }

        Users IUser.GetUserDetailbyEmailID(string email)
        {
            return userHandler.GetUserDetailbyEmailID(email);
        }

        public ForgotPasswordModel SaveForgotPassToken(ForgotPasswordModel model)
        {
            return userHandler.SaveForgotPassToken(model);
        }
        public ResetPasswordModel GetForgotPassDetailByToken(string token)
        {
            return userHandler.GetForgotPassDetailByToken(token);
        }

        public ResetPasswordModel SetResetPassword(ResetPasswordModel model)
        {
            return userHandler.SetResetPassword(model);
        }
    }
}
