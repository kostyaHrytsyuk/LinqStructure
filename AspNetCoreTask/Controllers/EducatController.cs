using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using AspNetCoreTask.Models;

namespace AspNetCoreTask.Controllers
{
    public class EducatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}