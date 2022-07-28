using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using VishBook.Models;
using VishBook.Repositories;
using VishBook.Models.ViewModel;

namespace VishBook.Controllers
{

    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IMoodRepository _moodRepository;
        private readonly IPostMoodRepository _postMoodRepository;

        public PostController(IPostRepository postRepository, IUserProfileRepository userProfileRepository, IMoodRepository moodRepository, IPostMoodRepository postMoodRepository)
        {
            _postRepository = postRepository;
            _userProfileRepository = userProfileRepository;
            _moodRepository = moodRepository;
            _postMoodRepository = postMoodRepository;
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
            var posts = new PostViewModel();
            posts.MoodOptions = _moodRepository.GetAllMood();
            return View(posts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel viewModel)
        {
            viewModel.MoodOptions = _moodRepository.GetAllMood();
            try
            {
                viewModel.postMood = new PostMood();
                viewModel.Post.UserId = GetCurrentUserId();
                _postRepository.Add(viewModel.Post);
                viewModel.postMood.PostId = viewModel.Post.Id;
                viewModel.postMood.MoodId = viewModel.SelectedMood.Id;
                _postMoodRepository.Add(viewModel.postMood);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(viewModel);
            }
        }
        [Authorize]
        public ActionResult Edit(int id)
        {
            PostViewModel viewModel = new PostViewModel();
            viewModel.Post = _postRepository.GetPostById(id);
            viewModel.postMood = new PostMood();

            if (viewModel.Post == null)
            {
                return NotFound();
            }
            if (viewModel.Post.UserId == GetCurrentUserId())
            {
                viewModel.MoodOptions = _moodRepository.GetAllMood();
                return View(viewModel);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        [Authorize]
        public ActionResult Edit(int id, PostViewModel viewModel)
        {
            try
            {
                viewModel.postMood = new PostMood();
                var pId = _postMoodRepository.GetPostMoodByPostId(viewModel.Post.Id);
                viewModel.postMood.Id = pId.Id;
                viewModel.MoodOptions = _moodRepository.GetAllMood();
                viewModel.Post.UserId = GetCurrentUserId();
                viewModel.postMood.PostId = viewModel.Post.Id;
                viewModel.postMood.MoodId = viewModel.SelectedMood.Id;
                _postRepository.Update(viewModel.Post);
                _postMoodRepository.Update(viewModel.postMood);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(viewModel);
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