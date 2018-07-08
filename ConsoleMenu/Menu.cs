using System;
using System.Threading;
using LinqStructure.Entities;

namespace LinqStructure
{
    static class Menu
    {
        private static LinqService _service = LinqService.Service;
        private static bool IsClosed = false;

        #region Menu Navigation
        public static void MenuMap()
        {
            Console.WriteLine("Program functionality:");
            Console.WriteLine("1. To get user's posts and number of comments to them - press 1");
            Console.WriteLine("2. To get a list of short comments for user's posts - press 2");
            Console.WriteLine("3. To get a list of user's done todos - press 3");
            Console.WriteLine("4. To get a list of users by alphabet - press 4");
            Console.WriteLine("5. To get an extended information about user - press 5");
            Console.WriteLine("6. To get an extended information about post - press 6");
            Console.WriteLine("7. To exit the program - press 0");

            Console.Write("Enter the value: ");
            var input = Char.GetNumericValue(Console.ReadKey().KeyChar);
            Console.Clear();

            switch (input)
            {
                case 1:
                    ShowNumberOfCommentsToUserPosts();
                    break;
                case 2:
                    ShowShortComments();
                    break;
                case 3:
                    ShowDoneTodos();
                    break;
                case 4:
                    ShowUsers();
                    break;
                case 5:
                    ShowUserX();
                    break;
                case 6:
                    ShowPostX();
                    break;
                case 0:
                    CloseProgram();
                    break;
                default:
                    break;
            }

            if (!IsClosed)
            {
                ReturnToMenu();
            }

            Console.Clear();
        }

        private static void ReturnToMenu()
        {
            Console.WriteLine("\nTo return to main menu - press R");
            var input = Console.ReadKey().Key.ToString();
            Console.WriteLine();
            if (input == "R")
            {
                Console.Clear();
                MenuMap();
            }
            else
            {
                Console.WriteLine("You entered a wrong value!\t");
                ReturnToMenu();
            }
        }

        private static void CloseProgram()
        {
            IsClosed = true;
            Console.Clear();
            Console.WriteLine("Have a nice day!");
            Thread.Sleep(3000);
        }
        #endregion

        #region Get Data Methods
        private static void ShowNumberOfCommentsToUserPosts()
        {
            var id = EnterId<User>();

            var posts = _service.GetUsersPosts(id);

            if (posts.Count != 0)
            {
                Console.WriteLine($"Number of comments for user {id} posts");
                foreach (var post in posts)
                {
                    Console.WriteLine($"Post: {post.Title}\tNumber of comments: {post.Comments.Count}");
                }
            }
            else
            {
                Console.WriteLine($"There is no posts for user {id}");
            }

            ReturnToMenu();
        }

        private static void ShowShortComments()
        {
            var id = EnterId<User>();

            var comments = _service.GetShortCommentsForUserPosts(id);

            if (comments.Count != 0)
            {
                Console.WriteLine($"Short comments under posts of {id} user:\n");
                foreach (var comment in comments)
                {
                    Console.WriteLine(comment.Body);
                }
            }
            else
            {
                Console.WriteLine($"User with {id} Id has no short comments for own posts");
            }
            Console.WriteLine();

            ReturnToMenu();
        }

        private static void ShowDoneTodos()
        {
            var id = EnterId<User>();

            var todos = _service.GetUsersTodosDone(id);

            if (todos.Count != 0)
            {
                foreach (var todo in todos)
                {
                    Console.WriteLine($"Todo Id: {todo.Id}\t Todo Name: {todo.Name}");
                }
            }
            else
            {
                Console.WriteLine($"User with {id} Id has no done todo");
            }

            ReturnToMenu();
        }

        private static void ShowUsers()
        {
            var users = _service.GetUsersSortedByAlphabet();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\t\tUsers");
            Console.ForegroundColor = ConsoleColor.Gray;
            foreach (var user in users)
            {
                Console.Write($"\n\t{user.Name}");
                if (user.Todos.Count > 0) { Console.WriteLine("\n\t\tUser Todos\n"); }
                foreach (var todo in user.Todos)
                {
                    Console.WriteLine($"\t\t{todo.Name}");
                }
                Console.WriteLine();
            }
        }

        private static void ShowUserX()
        {
            var id = EnterId<User>();

            var userX = _service.GetUserX(id);

            Console.WriteLine($"User: {userX.User.Name}");
            Console.WriteLine($"Last Post: {userX.LastPost.Title}");
            Console.WriteLine($"Numbers Of Comments Of Last Post: {userX.NumbersOfCommentsOfLastPost}");
            Console.WriteLine($"Number of Undone Todos: {userX.NumbersOfCommentsOfLastPost}");
            Console.WriteLine($"The most popular post by comments: { (userX.BestPostByComments != null ? userX.BestPostByComments.Title : $"{userX.User.Name} has no comments for own posts")}");
            Console.WriteLine($"The most popular post by likes: {userX.BestPostByLikes.Title}");

            ReturnToMenu();
        }

        private static void ShowPostX()
        {
            var id = EnterId<Post>();

            var postX = _service.GetPostX(id);

            Console.WriteLine($"Post: {postX.Post.Title}");
            Console.WriteLine($"The Longest Comment: {postX.LongestComment.Body}");
            Console.WriteLine($"The Best Comment By Likes: {postX.BestCommentByLikes.Body}");
            Console.WriteLine($"Number of comments without likes and short: {postX.NumberOfShort_ZeroLikesComment}");

            ReturnToMenu();
        }
        #endregion
        
        private static int EnterId<T>()
        {
            var id = 0;
            Console.Write($"Enter { typeof(T).Name } Id.\nId must be less than 100.\nId: ");

            var input = Console.ReadLine();

            if (int.TryParse(input,out id) && (id > 0 && id < 100))
            {
                return id;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("You entered a wrong value!");
                Console.WriteLine(new string('-', 15));
                EnterId<T>();
            }

            Console.Clear();

            return id;
        }
    }
}
