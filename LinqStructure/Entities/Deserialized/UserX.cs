using System.ComponentModel;

namespace LinqStructure.Entities.Deserialized
{
    public struct UserX
    {
        public User User { get; }
        public Post LastPost { get; }
        public int NumbersOfCommentsOfLastPost { get; }
        public int UndoneTodosNumber { get; }
        public Post BestPostByComments { get; }
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
