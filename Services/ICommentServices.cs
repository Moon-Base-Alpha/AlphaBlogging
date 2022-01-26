using AlphaBlogging.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaBlogging.Data.Repos
{
    public interface ICommentServices
    {
        Comment GetComment(int Id);
        List<Comment> GetAllComments();
        public string GetCommentOwner(int Id);
        void AddComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(int Id);
        public string GetFirstPartOfComment(int commentId, int introLength);
        IEnumerable<Comment> GetCommentsFromPostID(int Id);

        IEnumerable<Comment> GetPostComments(int? Id);
        Task<bool> SaveChangesAsync();
    }
}
