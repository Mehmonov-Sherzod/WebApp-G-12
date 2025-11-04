using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using WebApp.Application.Common;

namespace WebApp.Application.Services.Impl
{
    public class EmailService : IEmailService
    {
        private readonly EmailConfiguration _config;

        public EmailService(IOptions<EmailConfiguration> config)
        {
            _config = config.Value;
        }
        public  bool SendOtpAsync(string toEmail, string otp)
        {
            try
            {
                using var client = new SmtpClient(_config.SmtpServer, _config.Port)
                {
                    EnableSsl = _config.EnableSsl,
                    Credentials = new NetworkCredential(_config.Username, _config.Password)
                };

                var message = new MailMessage
                {
                    From = new MailAddress(_config.DefaultFromEmail, _config.DefaultFromName),
                    Subject = "Test_App: OTP Verification Code",
                    Body = GenerateBody(otp),
                    IsBodyHtml = true
                };

                message.To.Add(toEmail);
                client.Send(message);
                return true;
            }
            catch (SmtpException smtpEx)
            {
                // SMTP ga oid xatolarni qayd qiling
                Console.WriteLine($"Elektron pochta yuborishda SMTP xatosi {toEmail} ga: {smtpEx.StatusCode} - {smtpEx.Message}");
                if (smtpEx.InnerException != null)
                {
                    Console.WriteLine($"Ichki istisno: {smtpEx.InnerException.Message}");
                }
                return false;
            }
            catch (Exception ex)
            {
                // Umumiy istisnolarni qayd qiling
                Console.WriteLine($"Elektron pochta yuborishda umumiy xato {toEmail} ga: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Ichki istisno: {ex.InnerException.Message}");
                }
                return false;
            }
        }

        private string GenerateBody(string otp)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang='uz'>");
            sb.AppendLine("<head>");
            sb.AppendLine("<meta charset='UTF-8' />");
            sb.AppendLine("<meta name='viewport' content='width=device-width, initial-scale=1.0' />");
            sb.AppendLine("<title>Test Yechish Sayti</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #f5f7fa; margin: 0; padding: 0; }");
            sb.AppendLine(".container { max-width: 500px; margin: 40px auto; background: white; border-radius: 12px; box-shadow: 0 4px 15px rgba(0,0,0,0.1); padding: 30px; text-align: center; }");
            sb.AppendLine(".title { font-size: 22px; color: #2d3748; margin-bottom: 10px; }");
            sb.AppendLine(".subtitle { color: #4a5568; margin-bottom: 25px; }");
            sb.AppendLine(".otp-box { background: #edf2f7; color: #2d3748; font-size: 32px; letter-spacing: 10px; font-weight: bold; border-radius: 8px; display: inline-block; padding: 15px 25px; }");
            sb.AppendLine(".footer { font-size: 12px; color: #a0aec0; margin-top: 25px; }");
            sb.AppendLine("</style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<div class='container'>");
            sb.AppendLine("<h2 class='title'>🧠 Test yechish saytiga xush kelibsiz!</h2>");
            sb.AppendLine("<p class='subtitle'>Sizning 4 xonali tasdiqlash kodingiz quyida:</p>");
            sb.AppendLine($"<div class='otp-box'>{otp}</div>");
            sb.AppendLine("<p style='margin-top:20px; color:#718096;'>Bu kod 5 daqiqa davomida amal qiladi. Hech kimga ulashmang!</p>");
            sb.AppendLine("<div class='footer'>&copy; 2025 TestEducation. Barcha huquqlar himoyalangan.</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</body></html>");
            return sb.ToString();
        }
    }
}
