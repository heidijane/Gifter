﻿using Gifter.Models;
using Gifter.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xunit;

namespace Gifter.Tests
{
    public class PostRepositoryTests : EFTestFixture
    {
        public PostRepositoryTests()
        {
            AddSampleData();
        }

        [Fact]
        public void Search_Should_Match_A_Posts_Title()
        {
            var repo = new PostRepository(_context);
            var results = repo.Search("Dude", false);

            Assert.Equal(2, results.Count);
            Assert.Equal("El Duderino", results[0].Title);
            Assert.Equal("The Dude", results[1].Title);
        }

        [Fact]
        public void Search_Should_Match_A_Posts_Caption()
        {
            var repo = new PostRepository(_context);
            var results = repo.Search("it is no dream", false);

            Assert.Equal(1, results.Count);
            Assert.Equal("If you will it, Dude, it is no dream", results[0].Caption);
        }

        [Fact]
        public void Search_Should_Return_Empty_List_If_No_Matches()
        {
            var repo = new PostRepository(_context);
            var results = repo.Search("foobarbazcatgrill", false);

            Assert.NotNull(results);
            Assert.Empty(results);
        }

        [Fact]
        public void Search_Can_Return_Most_Recent_First()
        {
            var mostRecentTitle = "The Dude";
            var repo = new PostRepository(_context);
            var results = repo.Search("", true);

            Assert.Equal(4, results.Count);
            Assert.Equal(mostRecentTitle, results[0].Title);
        }

        [Fact]
        public void Search_Can_Return_Most_Recent_Last()
        {
            var mostRecentTitle = "It's my hobby";
            var repo = new PostRepository(_context);
            var results = repo.Search("", false);

            Assert.Equal(4, results.Count);
            Assert.Equal(mostRecentTitle, results[2].Title);
        }

        [Fact]
        public void User_Can_Delete_Post_With_Comment()
        {
            var postIdWithComment = 2;
            var repo = new PostRepository(_context);

            // Attempt to delete it
            repo.Delete(postIdWithComment);

            // Now attempt to get it
            var result = repo.GetById(postIdWithComment);

            Assert.Null(result);
        }

        [Fact]
        public void Posts_Ordered_Alphabetically_By_Title()
        {
            var repo = new PostRepository(_context);
            var results = repo.GetByUserProfileId(3);

            //put the results in alphabetical order by title so that we can compare the two lists
            List<Post> expectedList = results.OrderBy(p => p.Title).ToList();

            //Debug.WriteLine("Results:");
            //foreach (Post post in results)
            //{
            //    Debug.WriteLine(post.Title);
            //}
            //Debug.WriteLine("");
            //Debug.WriteLine("Ordered Output:");
            //foreach (Post post in expectedList)
            //{
            //    Debug.WriteLine(post.Title);
            //}

            Assert.True(expectedList.SequenceEqual(results));

        }

        [Fact]
        public void Posts_Belong_To_Specified_User()
        {
            var repo = new PostRepository(_context);

            int specifiedUserId = 3;
            var results = repo.GetByUserProfileId(specifiedUserId);
            foreach (Post post in results)
            {
                Assert.True(post.UserProfileId == specifiedUserId);
            }
        }

        [Fact]
        public void Return_Empty_List_When_No_Posts()
        {
            var repo = new PostRepository(_context);
            var results = repo.GetByUserProfileId(5); //this user doesn't exist

            Assert.True(results.GetType() == typeof(List<Post>)); //check to make sure variable is a list
            Assert.True(!results.Any()); //check if list has any elements
        }

        [Fact]
        public void GetMostRecent_Returns_One_Post()
        {
            var repo = new PostRepository(_context);
            var results = repo.GetMostRecent(1);

            Assert.True(results.Count == 1);
        }

        [Fact]
        public void GetMostRecent_Returns_Three_Posts()
        {
            var repo = new PostRepository(_context);
            var results = repo.GetMostRecent(3);

            Assert.True(results.Count == 3);
        }

        [Fact]
        public void GetMostRecent_Cant_Be_Greater_Than_Num_Of_Posts()
        {
            var repo = new PostRepository(_context);
            var results = repo.GetMostRecent(100);

            Assert.True(results.Count == 4);
        }

        [Fact]
        public void GetMostRecent_Less_Than_1_Returns_Empty()
        {
            var repo = new PostRepository(_context);
            var results = repo.GetMostRecent(0);

            Assert.True(!results.Any());
        }

        [Fact]
        public void GetMostRecent_In_Decending_Order_By_Date()
        {
            var repo = new PostRepository(_context);
            var results = repo.GetMostRecent(3); //this user has more than one post

            //put the list in the expected order so that we can compare
            List<Post> expectedList = results.OrderByDescending(p => p.DateCreated).ToList();

            Assert.True(expectedList.SequenceEqual(results));
        }

        // Add sample data
        private void AddSampleData()
        {
            var user1 = new UserProfile()
            {
                Name = "Walter",
                Email = "walter@gmail.com",
                DateCreated = DateTime.Now - TimeSpan.FromDays(365),
                FirebaseUserId = "a123456789"
            };

            var user2 = new UserProfile()
            {
                Name = "Donny",
                Email = "donny@gmail.com",
                DateCreated = DateTime.Now - TimeSpan.FromDays(400),
                FirebaseUserId = "b123456789"
            };

            var user3 = new UserProfile()
            {
                Name = "The Dude",
                Email = "thedude@gmail.com",
                DateCreated = DateTime.Now - TimeSpan.FromDays(400),
                FirebaseUserId = "c123456789"
            };

            _context.Add(user1);
            _context.Add(user2);
            _context.Add(user3);

            var post1 = new Post()
            {
                Caption = "If you will it, Dude, it is no dream",
                Title = "The Dude",
                ImageUrl = "http://foo.gif",
                UserProfile = user1,
                DateCreated = DateTime.Now - TimeSpan.FromDays(10)
            };

            var post2 = new Post()
            {
                Caption = "If you're not into the whole brevity thing",
                Title = "El Duderino",
                ImageUrl = "http://foo.gif",
                UserProfile = user2,
                DateCreated = DateTime.Now - TimeSpan.FromDays(11),
            };

            var post3 = new Post()
            {
                Caption = "It really ties the room together",
                Title = "My Rug",
                ImageUrl = "http://foo.gif",
                UserProfile = user3,
                DateCreated = DateTime.Now - TimeSpan.FromDays(12),
            };

            var post4 = new Post()
            {
                Caption = "Bowling",
                Title = "It's my hobby",
                ImageUrl = "http://foo.gif",
                UserProfile = user3,
                DateCreated = DateTime.Now - TimeSpan.FromDays(11),
            };

            var comment1 = new Comment()
            {
                Post = post2,
                Message = "This is great",
                UserProfile = user3
            };

            var comment2 = new Comment()
            {
                Post = post2,
                Message = "The post really tied the room together",
                UserProfile = user2
            };

            _context.Add(post1);
            _context.Add(post2);
            _context.Add(post3);
            _context.Add(post4);
            _context.Add(comment1);
            _context.Add(comment2);
            _context.SaveChanges();
        }
    }
}