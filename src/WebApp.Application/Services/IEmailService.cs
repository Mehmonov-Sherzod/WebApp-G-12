using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Application.Services
{
    public interface IEmailService
    {
        bool SendOtpAsync(string toEmail, string otp);
    }
}
