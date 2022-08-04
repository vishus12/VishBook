using System.Collections.Generic;

namespace VishBook.Models.ViewModel
{
    public class PostDetailViewModel
    {

        public Post Post { get; set; }

        public List<Mood> Moods { get; set; }
    }
}
