using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Application.Models.UserEmail
{
    public class UserEmailForgot
    {
        public string OtpCode { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
