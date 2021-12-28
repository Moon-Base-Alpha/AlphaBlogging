using AlphaBlogging.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaBlogging.Data.Repos
{
    public interface ICommentServices
    {
        Comment GetComment(int Id);
        List<Comment> GetAllComments();
        void AddComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(int Id);
        IEnumerable<Comment> GetCommentsFromPostID(int Id);

        IEnumerable<Comment> GetPostComments(int? Id);
        Task<bool> SaveChangesAsync();
    }
}
