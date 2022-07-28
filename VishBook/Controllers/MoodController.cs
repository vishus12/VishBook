using Microsoft.AspNetCore.Mvc;

namespace VishBook.Controllers
{
    public class MoodController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
