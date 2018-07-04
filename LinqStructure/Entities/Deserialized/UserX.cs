namespace LinqStructure.Entities.Deserialized
{
    public struct UserX
    {
        User User { get; }
        Post LastPost { get; }
        int NumbersOfCommentsOfLastPost { get; }
        int UndoneTodosNumber { get; }
        Post BestPostByComments { get; }
        Post BestPostByLikes { get; }

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
