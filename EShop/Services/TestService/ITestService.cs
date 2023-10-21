using EShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Services.TestService
{
    public interface ITestService
    {
        Task<ActionResult<IEnumerable<Test>>> GetAll(); 
    }
}
