namespace LinqService.Entities.Deserialized
{
    public struct PostX
    {
        public Post Post { get; }
        public Comment LongestComment { get; }
        public Comment BestCommentByLikes { get; }
        public int NumberOfShort_ZeroLikesComment { get; }
        
        public PostX(Post post, Comment longestComment, Comment bestCommentByLikes, int numberOfShort_ZeroLikesComment) : this()
        {
            Post = post;
            LongestComment = longestComment;
            BestCommentByLikes = bestCommentByLikes;
            NumberOfShort_ZeroLikesComment = numberOfShort_ZeroLikesComment;
        }
    }
}
