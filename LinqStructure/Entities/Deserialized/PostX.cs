namespace LinqStructure.Entities.Deserialized
{
    public struct PostX
    {
        Post Post { get; }
        Comment LongestComment { get; }
        Comment BestCommentByLikes { get; }
        int NumberOfShort_ZeroLikesComment { get; }
        
        public PostX(Post post, Comment longestComment, Comment bestCommentByLikes, int numberOfShort_ZeroLikesComment) : this()
        {
            Post = post;
            LongestComment = longestComment;
            BestCommentByLikes = bestCommentByLikes;
            NumberOfShort_ZeroLikesComment = numberOfShort_ZeroLikesComment;
        }
    }
}
