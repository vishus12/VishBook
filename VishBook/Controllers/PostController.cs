using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using VishBook.Models;
using VishBook.Repositories;

namespace VishBook.Controllers

{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserProfileRepository _userProfileRepository;
    public PostController(IPostRepository postRepository, IUserProfileRepository userProfileRepository)
    {
        _postRepository = postRepository;
        _userProfileRepository = userProfileRepository;
    }
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        public ActionResult Index()
        {
            int ownerId = GetCurrentUserId();

            List<Post> posts = _postRepository.GetPostByUserId(ownerId);

            return View(posts);

        }

    }
}
