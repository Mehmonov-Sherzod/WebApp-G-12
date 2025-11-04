using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.DataAccess.Persistence;
using WebApp.Domain.Entities;

namespace WebApp.Application.Services.Impl
{
    public class OtpService : IOtpService
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public OtpService(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public string GenerateAndSaveOtpAsync(int userId)
        {
            var user =  _context.Users.Find(userId);
            if (user == null)
                throw new Exception("User not found");

            var otpCode = new Random().Next(100000, 999999).ToString();

            var otp = new UserOTPs
            {
                UserId = userId,
                Code = otpCode,
                CreatedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddMinutes(5)
            };

             _context.userOTPs.Add(otp);
             _context.SaveChanges();

            return otpCode;
        }

        public UserOTPs? GetLatestOtpAsync(int userId, string code)
        {
             var result = _context.userOTPs
                .Where(o => o.UserId == userId && o.Code == code && o.ExpiredAt > DateTime.Now)
                .OrderByDescending(o => o.CreatedAt)
                .FirstOrDefault();

            return result;
        }
    }
}
