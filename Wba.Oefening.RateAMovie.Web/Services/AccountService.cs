using Isopoh.Cryptography.Argon2;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wba.Oefening.RateAMovie.Core.Entities;
using Wba.Oefening.RateAMovie.Web.Data;
using Wba.Oefening.RateAMovie.Web.Services.Interfaces;

namespace Wba.Oefening.RateAMovie.Web.Services
{
    public class AccountService : IAccountService
    {
        private readonly MovieContext _movieContext;

        public AccountService(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }
        public async Task<bool> Login(string username, string password)
        {
            var user = await _movieContext.Users.FirstOrDefaultAsync(u => u.Username.Equals(username));
            if(user == null || !Argon2.Verify(user.Password,password))
            {
                return false;
            }
            return true;
        }

        public async Task<bool> register(string username, string password, string firstname, string lastname)
        {
            if(await _movieContext.Users.AnyAsync(u => u.Username.Equals(username)))
            {
                return false;
            }
            var newUser = new User
            {
                Username = username,
                FirstName = firstname,
                LastName = lastname,
                Password = Argon2.Hash(password)
            };
            await _movieContext.Users.AddAsync(newUser);
            try {
                await _movieContext.SaveChangesAsync();
                return true;
            }
            catch(DbUpdateException dbUpdateException)
            {
                Console.WriteLine(dbUpdateException);
                return false;
            }
        }
    }
}
