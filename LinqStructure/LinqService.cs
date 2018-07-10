using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using LinqStructure.Entities;
using LinqStructure.Entities.Deserialized;
using Newtonsoft.Json;

namespace LinqStructure
{
    public sealed class LinqService
    {
        #region Service Creation
        private static readonly Lazy<LinqService> lazyService = new Lazy<LinqService>(() => new LinqService());

        public static LinqService Service { get { return lazyService.Value; } }

        private LinqService()
        {
            #region Comments
            Comments = DownloadApiDataByUrl<Comment>("comments");
            #endregion

            #region Posts
            var rawPosts = DownloadApiDataByUrl<RawPost>("posts");
            Posts = (from post in rawPosts
                     join comment in Comments on post.Id equals comment.PostId into postComments
                     select new Post()
                     {
                         Id = post.Id,
                         CreatedAt = post.CreatedAt,
                         Title = post.Title,
                         Body = post.Body,
                         UserId = post.UserId,
                         Likes = post.Likes,
                         Comments = postComments.ToList()
                     }).ToList();
            #endregion

            #region Todos
            Todos = DownloadApiDataByUrl<Todo>("todos");
            #endregion

            #region Users
            var rawUsers = DownloadApiDataByUrl<RawUser>("users");
            Users = (from user in rawUsers
                     join post in Posts on user.Id equals post.UserId into userPosts
                     join todo in Todos on user.Id equals todo.UserId into userTodos
                     join comment in Comments on user.Id equals comment.UserId into userComments
                     select new User()
                     {
                         Id = user.Id,
                         CreatedAt = user.CreatedAt,
                         Name = user.Name,
                         Posts = userPosts.ToList(),
                         Avatar = user.Avatar,
                         Email = user.Email,
                         Todos = userTodos.ToList()
                     }).ToList();
            #endregion
        }
        #endregion

        #region Data
        public List<Comment> Comments { get; }
        public List<Post> Posts { get; }
        public List<Todo> Todos { get; }
        public List<User> Users { get; }
        #endregion

        #region Get Data Methods
        public Todo GetTodo(int id)
        {
            return Todos.Where(t => t.Id == id).FirstOrDefault();
        }

        public Post GetPost(int id)
        {
            return Posts.Where(p => p.Id == id).FirstOrDefault();
        }

        public User GetUser(int id)
        {
            return Users.Where(u => u.Id == id).FirstOrDefault();
        }

        public List<Post> GetUsersPosts(int userId)
        {
   

            var usersPosts = (from post in Posts
                              where post.UserId == userId
                              select post).ToList();

            return usersPosts;
        }

        public List<Comment> GetShortCommentsForUserPosts(int userId)
        {
            var shortComments = Posts.Where(p => p.UserId == userId).FirstOrDefault().Comments.Where(c => c.Body.Length < 50).ToList();

            return shortComments;
        }

        public List<Todo> GetUserTodosDone(int userId)
        {
            var userTodosDone = (from todo in Todos
                                 where todo.UserId == userId && todo.IsComplete == true
                                 select todo).ToList();

            return userTodosDone;
        }

        public List<User> GetUsersSortedByAlphabet()
        {
            Users.OrderBy(u => u.Name).ToList().ForEach(u => u.Todos.OrderByDescending(t => t.Name.Length));
            
            return Users;
        }

        public UserX GetUserX(int userId)
        {
            var userX = (from user in Users
                        where user.Id == userId
                        select new UserX(user,
                                         user.Posts.OrderByDescending(p => p.CreatedAt).FirstOrDefault(),
                                         user.Todos.Where(t => t.IsComplete == false).Count(),
                                         user.Posts.OrderByDescending(p => p.Comments.Where(c => c.Body.Length > 80).Count()).FirstOrDefault(),
                                         user.Posts.OrderByDescending(p => p.Likes).FirstOrDefault())
                        ).FirstOrDefault();
           
            return userX;
        }

        public PostX GetPostX(int postId)
        {
            var postX = (from post in Posts
                         where post.Id == postId
                         select new PostX(post,
                                          post.Comments.OrderByDescending(c => c.Body.Length).FirstOrDefault(),
                                          post.Comments.OrderByDescending(c => c.Likes).FirstOrDefault(),
                                          post.Comments.Where(c => c.Likes == 0 || c.Body.Length < 80).Count())
                                         ).FirstOrDefault();

            return postX;
        }
        #endregion

        #region Downloading data from API
        private static List<T> DownloadApiDataByUrl<T>(string urlParam) where T : new()
        {
            using (var w = new WebClient())
            {
                var data = string.Empty;
                var url = "https://5b128555d50a5c0014ef1204.mockapi.io/" + urlParam;
                try
                {
                    data = w.DownloadString(url);
                }
                catch (Exception)
                {
                    Console.WriteLine($"API source page {urlParam} not found");
                }

                return !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<List<T>>(data) : new List<T>();
            }
        }
        #endregion
    }
}
