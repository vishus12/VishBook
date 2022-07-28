using System.Collections.Generic;
using VishBook.Models;

namespace VishBook.Repositories
{
    public interface IPostMoodRepository
    {
        public List<PostMood> GetAllPostMood();
        PostMood GetPostMoodById(int id);

        void Add(PostMood postmood);

        public void Update(PostMood postmood);

        public PostMood GetPostMoodByPostId(int id);
    };
}
