using InstaSharp.Data.Context;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InstaSharp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly InstaDbContext _ctx = new InstaDbContext();

        public async Task<ActionResult> Index()
        {
            var posts = await _ctx.Posts.OrderByDescending(p => p.Timestamp).ToListAsync();
            return View(posts);
        }
    }
}