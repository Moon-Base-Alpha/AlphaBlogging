using AlphaBlogging.Data.Repos;
using AlphaBlogging.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AlphaBlogging.Controllers
{
    public class PostController : Controller
    {
        private IPostServices _repo;
        public PostController(IPostServices repo)
        {
            _repo = repo;
        }
        public IActionResult Postlist()
        {
            var posts = _repo.GetAllPosts();
            return View(posts);
        }
        public IActionResult Post(int id) 
        {
            var post = _repo.GetPost(id);
            return View(post); 
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Post());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Post post)
        {
            _repo.AddPost(post);
            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Edit");
            else
                return View(post);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new Post());
            else
            {
                var post = _repo.GetPost((int)id);
                return View(post);
            }           
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if (post.Id > 0)
                _repo.UpdatePost(post);
            else
            {
                _repo.AddPost(post);
            }
           
            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Edit");
            else
                return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int id)
        {
            _repo.DeletePost(id);
            await _repo.SaveChangesAsync();
            return RedirectToAction("Postlist");
        }

        //// GET: PostController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //// GET: PostController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: PostController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: PostController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PostController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: PostController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: PostController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: PostController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
