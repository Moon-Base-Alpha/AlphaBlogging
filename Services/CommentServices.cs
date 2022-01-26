using AlphaBlogging.Models;
using AlphaBlogging.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaBlogging.Data.Repos
{
    public class CommentServices : ICommentServices
    {
        private const int StartIndex = 0;
        private readonly ApplicationDbContext _db;

        public CommentServices(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Comment> Comments { get;}

        public IEnumerable<Comment> GetCommentsFromPostID(int commentId)
        {
            List<Comment> resultList = new();

            var temp = (from x in _db.Posts
                        where x.Id == commentId
                        select x.Comments).First();

            if (temp != null)
            {
                resultList = temp.ToList();
            }
            return resultList;
        }
        public string GetFirstPartOfComment(int commentId, int introLength)
        {
            var commentIntro = _db.Comments.Where(c => c.Id == commentId).Select(c => c.Body).FirstOrDefault();

            return commentIntro[..introLength];
        }


        public string GetCommentOwner(int commentId)
        {            
            var userName = _db.Comments.Where(c => c.Id == commentId).Select(c => c.Author).ToList();
            return userName.ElementAt(0).UserName;    
        }

        public void AddComment(Comment comment)
        {
            comment.Updated = DateTime.Now;
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

        public IEnumerable<Comment> GetPostComments(int? Id)
        {
            return _db.Comments.Where(c => c.PostId == Id).ToList();
        }

        public Comment GetComment(int id)
        {
            return _db.Comments.FirstOrDefault(c => c.Id == id);
        }

        public void UpdateComment(Comment comment)
        {
            comment.Updated = DateTime.Now;
            comment.Created = (from x in _db.Comments
                            where x.Id == comment.Id
                            select x.Created).First();

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
