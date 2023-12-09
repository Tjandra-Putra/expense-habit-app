using FinMarketApp.Server.Data;
using FinMarketApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinMarketApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // constructor
        public CategoryController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // ================================ Get Categories ================================
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
          // get categories from database
            var categories = await _context.Category.ToListAsync();
            return Ok(categories);
        }
    }
}
