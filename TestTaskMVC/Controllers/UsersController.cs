using Microsoft.AspNetCore.Mvc;

namespace TestTaskMVC.Controllers
{
    public class UsersController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
