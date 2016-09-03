using InstaSharp.Data.Context;
using InstaSharp.Data.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InstaSharp.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly InstaDbContext _ctx = new InstaDbContext();

        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var post = await _ctx.Posts.FindAsync(id);
            if (post == null)
                return HttpNotFound();

            return View(post);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Post post, HttpPostedFileBase imageFile)
        {
            // Store time posted
            post.Timestamp = DateTime.Now;
            post.User = await _ctx.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);

            // Save the image to server
            if (imageFile.ContentLength > 0)
            {
                var fileName = Path.GetRandomFileName().Replace(".", "") + ".png";
                var directory = Server.MapPath(String.Format("~/Images/Uploads/{0}/", User.Identity.Name));
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                var path = Path.Combine(directory, fileName);
                imageFile.SaveAs(path);
                post.Image = fileName;
            }

            // Store post in db
            if (ModelState.IsValid)
            {
                _ctx.Posts.Add(post);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            return View(post);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = await _ctx.Posts.FindAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Post post = await _ctx.Posts.FindAsync(id);
            _ctx.Posts.Remove(post);
            await _ctx.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _ctx.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
