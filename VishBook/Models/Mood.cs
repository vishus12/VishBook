using System.ComponentModel.DataAnnotations;

namespace VishBook.Models
{
    public class Mood
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
