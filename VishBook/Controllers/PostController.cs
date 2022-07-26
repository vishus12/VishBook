using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            try
            {
                post.UserId = GetCurrentUserId();
                _postRepository.Add(post);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(post);
            }
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            Post post = _postRepository.GetPostById(id);

            if (post == null)
            {
                return NotFound();
            }
            if (post.UserId == GetCurrentUserId())
            {
                return View(post);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit(int id, Post post)
        {
            try
            {
                _postRepository.Update(post);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(post);
            }
        }
        [Authorize]
        public ActionResult Delete(int id)
        {
            Post post = _postRepository.GetPostById(id);

            if (post.UserId == GetCurrentUserId())
            {
                return View(post);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post post)
        {
            try
            {
                _postRepository.Delete(post);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(post);
            }
        }


    }
}
