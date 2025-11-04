using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Entities;

namespace WebApp.Application.Services
{
    public interface IOtpService
    {
        string GenerateAndSaveOtpAsync(int userId);
        UserOTPs? GetLatestOtpAsync(int userId, string code);
    }
}
