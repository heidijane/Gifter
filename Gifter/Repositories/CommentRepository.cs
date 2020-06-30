using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Gifter.Data;
using Gifter.Models;

namespace Gifter.Repositories
{
    public class CommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Comment> GetAll()
        {
            return _context.Comment.Include(c => c.UserProfile).Include(c => c.Post).ToList();
        }

        public Comment GetById(int id)
        {
            return _context.Comment.Include(c => c.UserProfile).Include(c => c.Post).FirstOrDefault(p => p.Id == id);
        }

        public List<Comment> GetByPostId(int id)
        {
            return _context.Comment.Include(c => c.UserProfile)
                                   .Include(c => c.Post)
                                   .Where(c => c.PostId == id)
                                   .OrderBy(c => c.Id)
                                   .ToList();
        }

        public void Add(Comment comment)
        {
            _context.Add(comment);
            _context.SaveChanges();
        }

        public void Update(Comment comment)
        {
            _context.Entry(comment).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var comment = GetById(id);
            _context.Comment.Remove(comment);
            _context.SaveChanges();
        }
    }
}