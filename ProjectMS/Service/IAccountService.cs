using ProjectMS.Models;

namespace ProjectMS.Service
{
    public interface IAccountService
    {
        bool CheckEmailExists(string email);
        ForgotPasswordModel SaveForgotPassToken(ForgotPasswordModel model);
        ResetPasswordModel GetForgotPassDetailByToken(string token);
        ResetPasswordModel SetResetPassword(ResetPasswordModel model);
    }
}
