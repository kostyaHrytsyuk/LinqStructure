﻿using Microsoft.AspNetCore.Mvc;
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

        #region Views For Service Methods
        #region Post
        public IActionResult SearchPosts()
        {
            return View();
        }

        public IActionResult GetUserPosts(int userId)
        {
            var posts = _service.GetUsersPosts(userId);
            return View("PostDetails", posts);
        }

        #endregion

        #region Comment
        public IActionResult SearchComments()
        {
            return View();
        }

        public IActionResult GetCommentsForUserPosts(int userId)
        {
            var comments = _service.GetShortCommentsForUserPosts(userId);
            return View("CommentDetails", comments);
        }
        #endregion

        #region Todos
        public IActionResult SearchTodos()
        {
            return View();
        }

        public IActionResult GetDoneTodos(int userId)
        {
            var todos = _service.GetUserTodosDone(userId);
            return View("TodoDetails",todos);
        }
        #endregion

        #region PostX
        public IActionResult SearchPostX()
        {
            return View();
        }

        public IActionResult PostXDetails(int postId)
        {
            var postX = _service.GetPostX(postId);
            return View("PostXDetails", postX);
        }

        #endregion

        #region UserX
        public IActionResult SearchUserX()
        {
            return View("SearchUserX");
        }

        public IActionResult UserXDetails(int userId)
        {
            var user = _service.GetUserX(userId);
            return View("UserXDetails", user);
        }
        #endregion
        #endregion


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}