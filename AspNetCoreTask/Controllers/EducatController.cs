using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using LinqStructure;
using AspNetCoreTask.Models;
using System;

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

        public IActionResult SearchUserX()
        {
            return View("SearchUserX");
        }

        public IActionResult UserXDetails(int userId)
        {
            var user = _service.GetUserX(userId);
            return View("UserXDetails",user);
        }

        public IActionResult SearchPostX()
        {
            return View();
        }

        public IActionResult PostXDetails(int postId)
        {
            var postX = _service.GetPostX(postId);
            return View("PostXDetails", postX);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}