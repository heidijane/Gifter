using System;
using System.ComponentModel.DataAnnotations;

namespace Gifter.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public int UserProfileId { get; set; }

        public UserProfile UserProfile { get; set; }

        [Required]
        public int PostId { get; set; }

        public Post Post { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Message { get; set; }
    }
}