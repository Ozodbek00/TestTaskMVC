using Microsoft.AspNetCore.Mvc;

namespace TestTaskMVC.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
