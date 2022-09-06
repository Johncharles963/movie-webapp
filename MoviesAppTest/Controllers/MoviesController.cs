using Microsoft.AspNetCore.Mvc;
using MoviesAppTest.Data;
using MoviesAppTest.Models;

namespace MoviesAppTest.Controllers
{

    public class MoviesController : Controller
    {
        public int test;
        private readonly MovieDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;
        public MoviesController(MovieDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Movie> movieList = _db.Movies;
            return View(movieList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Movie obj, IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {              
                    string fileName = Guid.NewGuid().ToString();
                    string upload = Path.Combine(wwwRootPath, @"images");
                    string fileExtenstion = Path.GetExtension(file.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(upload,fileName+fileExtenstion), FileMode.Create ))
                    {
                        file.CopyTo(fileStreams);
                        obj.ImgUrl = @"images\" + fileName + fileExtenstion;
                    }
                }
                _db.Add(obj);
                _db.SaveChanges();
                TempData["success"]= "Movie added Successfully";
                return RedirectToAction("Index");
            }
            else return View(obj);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var movieObj = _db.Movies.Find(id);
            return View(movieObj);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Movie obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    string upload = Path.Combine(wwwRootPath, @"images");
                    string fileExtenstion = Path.GetExtension(file.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(upload, fileName + fileExtenstion), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                        obj.ImgUrl = @"images\" + fileName + fileExtenstion;
                    }
                }
                _db.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Movie updated Successfully";
                return RedirectToAction("Index");
            }
            else return View(obj);
        }
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movieObj = _db.Movies.Find(id);
            return View(movieObj);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Movie obj)
        {     
            if(obj.ImgUrl != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string upload = Path.Combine(wwwRootPath, obj.ImgUrl);
                if (System.IO.File.Exists(upload))
                {
                    System.IO.File.Delete(upload);
                }
            }
            _db.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Movie Deleted Succesfully";
            return RedirectToAction("Index");
        }
    }

}
