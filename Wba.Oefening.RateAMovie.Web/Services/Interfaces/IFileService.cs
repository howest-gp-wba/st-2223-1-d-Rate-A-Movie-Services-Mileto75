using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Wba.Oefening.RateAMovie.Web.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> AddOrUpdateFile(IFormFile file, string subPath, IWebHostEnvironment webHostEnvironment, string fileName = "");
    }
}