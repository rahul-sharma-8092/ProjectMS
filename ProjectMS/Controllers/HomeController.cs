using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ProjectMS.Models;
using Microsoft.AspNetCore.Authentication;
using ProjectMS.Service;
using System.Diagnostics;

namespace ProjectMS.Controllers
{
    public class HomeController : Controller
    {
        #region Constructor
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        private readonly IEmailService emailService;
        private readonly IAccountService accountService;
        private readonly IEncryptionService encryption;

        public HomeController(IConfiguration _configuration, IEmailService _emailService, IUserService _userService, IAccountService _accountService, IEncryptionService _encryption)
        {
            configuration = _configuration;
            emailService = _emailService;
            encryption = _encryption;
            userService = _userService;
            accountService = _accountService;
        }
        #endregion

        [HttpGet]
        public IActionResult Index()
        {
            bool IsAuthenticated = User.Identity.IsAuthenticated;
            if (IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Login login)
        {
            if (ModelState.IsValid)
            {
                Users users = userService.GetUserDetailbyEmailID(login.EmailId);
                if (users != null && !string.IsNullOrEmpty(users.EmailID))
                {
                    bool IsPasswordValid = encryption.VerifyHashBCrypt(login.Password, users.Password);
                    if (IsPasswordValid)
                    {
                        //User Logged-In
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, string.Concat(users.FirstName, " ", users.LastName)),
                            new Claim(ClaimTypes.Email, users.EmailID),
                            new Claim(ClaimTypes.Role, users.Role ?? "Admin")
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5),
                            IsPersistent = login.RememberMe,
                            RedirectUri = "/Home/Dashboard",
                            AllowRefresh = true,
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                        return RedirectToAction("Dashboard", "Home");
                    }
                    else
                    {
                        //Wrong Password
                        TempData["Error"] = "Invalid Creditinals.";
                    }
                }
                else
                {
                    //Email-Id Not Registered
                    ModelState.AddModelError("EmailId", "Email ID not registered.");
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            if (accountService.CheckEmailExists(Email))
            {
                ForgotPasswordModel forgotPassword = new ForgotPasswordModel();

                Guid guid = Guid.NewGuid();
                forgotPassword.Token = guid.ToString();
                forgotPassword.Email = Email;
                forgotPassword.IPAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? HttpContext.Connection.RemoteIpAddress?.ToString();

                forgotPassword = accountService.SaveForgotPassToken(forgotPassword);
                string resetLink = Url.Action("ResetPassword", "Home", new { }, Request.Scheme) + "/" + forgotPassword.Token;

                string emailBody = System.IO.File.ReadAllText("F:\\Asp Dot Net MVC\\MVC\\ProjectMS-X\\ProjectMS\\wwwroot\\EmailTemplates\\PasswordReset.html");

                emailBody = emailBody.Replace("{{resetLink}}", resetLink);
                emailBody = emailBody.Replace("{{Name}}", forgotPassword.FullName);

                await emailService.SendEmailAsync(forgotPassword.Email, "Password Reset Request", emailBody);
                
                TempData["Success"] = "Password reset link send to your email.";

                return RedirectToAction("Index", "Home");
            }
            TempData["Error"] = "Email Id may be not registered or invalid.";
            ViewBag.EmailID = Email;
            return View();
        }


        [HttpGet]
        public IActionResult ResetPassword(string id)
        {
            ResetPasswordModel resetPassword = accountService.GetForgotPassDetailByToken(id);

            return View(resetPassword);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string? id, ResetPasswordModel resetPassword)
        {
            ResetPasswordModel resetPasswordPrev = accountService.GetForgotPassDetailByToken(id);

            if (ModelState.IsValid && (resetPasswordPrev.Email == resetPassword.Email))
            {
                resetPassword.Password = encryption.CreateHashBCrypt(resetPassword.Password);
                resetPassword = accountService.SetResetPassword(resetPassword);

                string emailBody = System.IO.File.ReadAllText("F:\\Asp Dot Net MVC\\MVC\\ProjectMS-X\\ProjectMS\\wwwroot\\EmailTemplates\\PasswordChanged.html");

                emailBody = emailBody.Replace("{{FullName}}", resetPassword.FullName);

                await emailService.SendEmailAsync(resetPassword.Email.Trim(), "Your Password has been changed.", emailBody);

                TempData["Success"] = "Your Password has been Changed.";

                return RedirectToAction("Index", "Home");
            }
            resetPassword.Email = resetPasswordPrev.Email;
            ViewBag.SomethingWentWrong = "Some thing went wrong.";
            return View(resetPassword);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
