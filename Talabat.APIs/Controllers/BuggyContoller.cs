using Microsoft.AspNetCore.Mvc;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
    public class BuggyContoller : Controller
    {
        private readonly TalabatApiMarchDbContext context;

        public BuggyContoller(TalabatApiMarchDbContext _context)
        {
            context = _context;
        }
        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            var product = context.Products.Find(100);
            var result = product.ToString();
            return Ok(result);
        }
    }
}
