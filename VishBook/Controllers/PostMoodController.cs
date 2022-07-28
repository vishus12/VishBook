using Microsoft.AspNetCore.Mvc;

namespace VishBook.Controllers
{
    public class PostMoodController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
