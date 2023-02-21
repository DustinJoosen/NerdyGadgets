using Microsoft.EntityFrameworkCore;
using NerdyGadgets.Data;
using NerdyGadgets.Dtos;
using NerdyGadgets.Models;

namespace NerdyGadgets.Services
{
    public class AuthService : BaseService
    {
        public AuthService(ApplicationDbContext context) : base(context)
        {
        }

        public bool CheckLogin(AuthDto auth, out User? user)
        {
            user = _context.Users
                .FirstOrDefault(u => u.Email == auth.EmailAddress);

            if (user == null || !BCrypt.Net.BCrypt.Verify(auth.Password, user.Password))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> EmailInUse(string email)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
            return user == null;
        }

        public static string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
