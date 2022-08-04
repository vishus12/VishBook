using System.Collections.Generic;

namespace VishBook.Models.ViewModel
{
    public class PostIndexViewModel
    {

        Post Post { get; set; }
        List<int> MoodIds { get; set; } 

        List<Mood> Moods { get; set; }
    }
}
