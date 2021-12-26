using AlphaBlogging.Models;
using AlphaBlogging.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaBlogging.Data.Repos
{
    public class CommentServices : ICommentServices
    {
        private ApplicationDbContext _db;

        public CommentServices(ApplicationDbContext db)
        {
            _db = db;
        }
        public void AddComment(Comment comment)
        {
            _db.Comments.Add(comment);
        }

        public void DeleteComment(int id)
        {
            _db.Comments.Remove(GetComment(id));
        }

        public List<Comment> GetAllComments()
        {
            return _db.Comments.ToList();
        }

        public Comment GetComment(int id)
        {
            return _db.Comments.FirstOrDefault(c => c.Id == id);
        }

        public void UpdateComment(Comment comment)
        {
            _db.Comments.Update(comment);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _db.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
