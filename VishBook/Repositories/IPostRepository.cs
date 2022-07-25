using System.Collections.Generic;
using System.IO;
using VishBook.Models;




namespace VishBook.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        void Update(Post post);
        void Delete(Post post);
        List<Post> GetAllPosts();
        Post GetPostById (int id);
        List<Post> GetPostByUserId(int userId);



    }
}
