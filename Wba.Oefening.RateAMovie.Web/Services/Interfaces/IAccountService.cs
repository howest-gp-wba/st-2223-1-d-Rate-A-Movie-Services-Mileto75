using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wba.Oefening.RateAMovie.Web.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> Login(string username, string password);
        Task<bool> register(string username, string password, string firstname, string lastname);
    }
}
