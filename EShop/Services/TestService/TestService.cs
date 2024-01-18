using EShop.Data;
using EShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShop.Services.TestService
{
    public class TestService : ITestService
    {
        private readonly EShopDBContext _context;
        public TestService(EShopDBContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<Test>>?> GetAll()
        {

            var result = await _context.Tests.ToListAsync();
            return result;

        }
    }
}
