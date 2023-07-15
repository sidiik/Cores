using Microsoft.AspNetCore.Mvc;
using SimpleProject.Data;
using SimpleProject.Data.Models;

namespace SimpleProject.Controllers
{
    [Route("[controller]")]
    public class SeedController : Controller
    {
        private readonly DataContext _context;
        public SeedController(DataContext context)
        {
            _context = context;
        }

    }
}