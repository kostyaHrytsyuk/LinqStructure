using System.ComponentModel;

namespace LinqStructure.Entities.Deserialized
{
    public struct UserX
    {
        public User User { get; }
        [DisplayName("Last Post")]
        public Post LastPost { get; }
        [DisplayName("Numbers Of Comments Of Last Post")]
        public int NumbersOfCommentsOfLastPost { get; }
        [DisplayName("Undone Todos Number")]
        public int UndoneTodosNumber { get; }
        [DisplayName("Best Post By Comments")]
        public Post BestPostByComments { get; }
        [DisplayName("Best Post By Likes")]
        public Post BestPostByLikes { get; }

        public UserX(User user, Post lastPost, int numbersOfCommentsOfLastPost, int undoneTodosNumber, Post bestPostByComments, Post bestPostByLikes) : this()
        {
            User = user;
            LastPost = lastPost;
            NumbersOfCommentsOfLastPost = numbersOfCommentsOfLastPost;
            UndoneTodosNumber = undoneTodosNumber;
            BestPostByComments = bestPostByComments;
            BestPostByLikes = bestPostByLikes;
        }
    }
}
