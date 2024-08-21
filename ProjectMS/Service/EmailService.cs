using MailKit.Net.Smtp;
using MimeKit;

namespace ProjectMS.Service
{
    public class EmailService : IEmailService
    {
        #region Constructor
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration _configuration)
        {
             configuration = _configuration;
        }
        #endregion

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpServer = configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"] ?? "0");
            var smtpUsername = configuration["EmailSettings:SmtpUsername"];
            var smtpPassword = configuration["EmailSettings:SmtpPassword"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("ProjectMS | Rahul Sharma", smtpUsername));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(smtpUsername, smtpPassword);
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while sending email: {ex.Message}");
                }
                finally
                {
                    await client.DisconnectAsync(true);
                }
            }
        }
    }
}
