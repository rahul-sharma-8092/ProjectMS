using ProjectMS.Models;

namespace ProjectMS.Repository
{
    public interface IAccountRepository
    {
        bool CheckEmailExists(string email);
        ForgotPasswordModel SaveForgotPassToken(ForgotPasswordModel model);
        ResetPasswordModel GetForgotPassDetailByToken(string token);
        ResetPasswordModel SetResetPassword(ResetPasswordModel model);
    }
}
