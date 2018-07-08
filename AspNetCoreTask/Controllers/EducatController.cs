using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LinqStructure;
using AspNetCoreTask.Models;

namespace AspNetCoreTask.Controllers
{
    public class EducatController : Controller
    {
        private static LinqService _service;

        public EducatController()
        {
            _service = LinqService.Service;
        }

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