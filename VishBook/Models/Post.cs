using System;
using System.ComponentModel.DataAnnotations;

namespace VishBook.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreateDateTime { get; set; }

        public int UserId { get; set; }
    }
}