using ProjectMS.Models;

namespace ProjectMS.Repository
{
    public interface IUser
    {
        public bool CheckEmailExists(string email);
        public Users GetUserDetailbyEmailID(string email);
        public ForgotPasswordModel SaveForgotPassToken(ForgotPasswordModel model);
        public ResetPasswordModel GetForgotPassDetailByToken(string token);
        public ResetPasswordModel SetResetPassword(ResetPasswordModel model);
    }
}
