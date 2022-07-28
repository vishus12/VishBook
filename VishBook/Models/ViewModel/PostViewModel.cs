﻿using System.Collections.Generic;

namespace VishBook.Models.ViewModel
{
    public class PostViewModel
    {

        public Post Post { get; set; }
        public List<Mood> MoodOptions { get; set; }

        public Mood SelectedMood { get; set; }

        public PostMood postMood { get; set; }
    }
}
