using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Gifter.Data;
using Gifter.Models;
using Microsoft.AspNetCore.JsonPatch.Internal;
using System;

namespace Gifter.Repositories
{
    public class PostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Post> GetAll()
        {
            return _context.Post.Include(p => p.UserProfile)
                                .Include(p => p.Comments)
                                .OrderByDescending(p => p.DateCreated)
                                .ToList();
        }

        public Post GetById(int id)
        {
            return _context.Post.Include(p => p.UserProfile).Include(p => p.Comments).FirstOrDefault(p => p.Id == id);
        }

        public List<Post> GetByUserProfileId(int id)
        {
            return _context.Post.Include(p => p.UserProfile)
                            .Include(p => p.Comments)
                            .Where(p => p.UserProfileId == id)
                            .OrderBy(p => p.Title)
                            .ToList();
        }

        public List<Post> Search(string criterion, bool sortDescending)
        {
            var query = _context.Post
                                .Include(p => p.UserProfile)
                                .Where(p => p.Title.Contains(criterion) || p.Caption.Contains(criterion));

            return sortDescending
                ? query.OrderByDescending(p => p.DateCreated).ToList()
                : query.OrderBy(p => p.DateCreated).ToList();
        }

        public List<Post> Hottest(string hotDate)
        {
            DateTime searchDate = DateTime.Now;
            try
            {
                //try to convert string to a date
                searchDate = DateTime.Parse(hotDate);
            } catch
            {
                //if it doesn't work make the datetime today - 24 hours
                searchDate = searchDate.AddHours(-24);
            }

            return _context.Post.Include(p => p.UserProfile)
                            .Include(p => p.Comments)
                            .Where(p => p.DateCreated >= searchDate)
                            .OrderBy(p => p.DateCreated)
                            .ToList();
        }

        public void Add(Post post)
        {
            _context.Add(post);
            _context.SaveChanges();
        }

        public void Update(Post post)
        {
            _context.Entry(post).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var post = GetById(id);
            _context.Post.Remove(post);
            _context.SaveChanges();
        }
    }
}