using ProjectMS.Models;
using ProjectMS.Repository;

namespace ProjectMS.Service
{
    public class AccountService : IAccountService
    {
        #region Constructor
        private readonly IAccountRepository accountRepository;
        public AccountService(IAccountRepository _accountRepository)
        {
            accountRepository = _accountRepository;
        }
        #endregion

        public bool CheckEmailExists(string email)
        {
            return accountRepository.CheckEmailExists(email);
        }

        public ResetPasswordModel GetForgotPassDetailByToken(string token)
        {
            return accountRepository.GetForgotPassDetailByToken(token);
        }

        public ForgotPasswordModel SaveForgotPassToken(ForgotPasswordModel model)
        {
            return accountRepository.SaveForgotPassToken(model);
        }

        public ResetPasswordModel SetResetPassword(ResetPasswordModel model)
        {
            return accountRepository.SetResetPassword(model);
        }
    }
}
