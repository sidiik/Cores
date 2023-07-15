using Microsoft.AspNetCore.Mvc;
using SimpleProject.Data;
using SimpleProject.Data.Models;

namespace SimpleProject.Controllers
{
    [Route("[controller]")]
    public class CityController : Controller
    {
        private readonly DataContext _context;

        public CityController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("all")]
        public async Task<ActionResult<ApiResult<City>>> GetAll(int pageIndex = 0,
         int pageSize = 10, string? sortColumn = null,
         string? sortOrder = null,
         string? filterColumn = null,
         string? filterQuery = null
         )
        {
            var result = await ApiResult<City>.CreateAsync(_context.Cities.AsNoTracking(), pageIndex, pageSize, sortColumn, sortOrder, filterColumn, filterQuery);
            return Ok(result);
        }

    }
}