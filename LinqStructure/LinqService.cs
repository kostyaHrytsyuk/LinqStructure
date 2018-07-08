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
        private List<Comment> Comments { get; }
        private List<Post> Posts { get; }
        private List<Todo> Todos { get; }
        private List<User> Users { get; }
        #endregion

        #region Get Data Methods
        public List<Post> GetUsersPosts(int userId)
        {
            var usersPosts = (from post in Posts
                              where post.UserId == userId
                              select post).ToList();

            return usersPosts;
        }

        public List<Comment> GetShortCommentsForUserPosts(int userId)
        {
            var usersPostsComments = from post in Posts
                                     where post.UserId == userId
                                     select post.Comments;

            var shortComments = new List<Comment>();
                        
            foreach (var comments in usersPostsComments)
            {
                var s = from comment in comments
                        where comment.Body.Length < 50
                        select comment;

                shortComments.AddRange(s);
            }

            return shortComments;
        }

        public List<Todo> GetUsersTodosDone(int userId)
        {
            var userTodosDone = (from todo in Todos
                                 where todo.UserId == userId && todo.IsComplete == true
                                 select todo).ToList();

            return userTodosDone;
        }

        public List<User> GetUsersSortedByAlphabet()
        {
            var usersSorted = (Users.OrderBy(u => u.Name)).ToList();
            usersSorted.ForEach(u => u.Todos = u.Todos.OrderByDescending(t => t.Name.Length).ToList());

            return usersSorted;
        }

        public UserX GetUserX(int userId)
        {
            var user = Users.Where(u => u.Id == userId).FirstOrDefault();

            var lastPost = (from post in user.Posts
                            where post.CreatedAt == user.Posts.Max(p => p.CreatedAt)
                            select post).FirstOrDefault();

            var undoneTodosNumber = user.Todos.Where(t => t.IsComplete == false).Count();

            var commentsForUserPosts = new List<Comment>();
            user.Posts.ForEach(p => commentsForUserPosts.AddRange(p.Comments));

            var bestPostByComments = (from post in user.Posts
                                      where post.Id == ((from comment in commentsForUserPosts
                                                         where comment.Body.Length == commentsForUserPosts.Max(c => c.Body.Length)
                                                         select comment.PostId).FirstOrDefault())
                                      select post).FirstOrDefault();

            var bestPostByLikes = (from post in user.Posts
                                   where post.Likes == user.Posts.Max(p => p.Likes)
                                   select post).FirstOrDefault();

            var userX = new UserX(user,
                                  lastPost,
                                  lastPost.Comments.Count,
                                  undoneTodosNumber,
                                  bestPostByComments,
                                  bestPostByLikes);

            return userX;
        }

        public PostX GetPostX(int postId)
        {
            var post = Posts.Where(p => p.Id == postId).FirstOrDefault();

            var longestComment = (from comment in post.Comments
                                  where comment.Body.Length == post.Comments.Max(c => c.Body.Length)
                                  select comment).FirstOrDefault();

            var bestCommentByLikes = (from comment in post.Comments
                                      where comment.Likes == post.Comments.Max(c => c.Likes)
                                      select comment).FirstOrDefault();

            var numberOfShort_ZeroLikesComment = (from comment in post.Comments
                                                  where comment.Likes == 0 || comment.Body.Length < 80
                                                  select comment).Count();
                        
            var postX = new PostX(post,
                                  longestComment,
                                  bestCommentByLikes,
                                  numberOfShort_ZeroLikesComment);

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
