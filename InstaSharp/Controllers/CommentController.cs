using InstaSharp.Data.Context;
using InstaSharp.Data.Models;
using InstaSharp.Services;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InstaSharp.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly InstaDbContext _ctx = new InstaDbContext();
        private readonly INotificationService _notificationService;

        public CommentController(INotificationService _notificationService)
        {
            this._notificationService = _notificationService;
        }

        [HttpPost]
        public async Task<ActionResult> Add(int postId, string comment)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            var newComment = new Comment
            {
                Message = comment,
                Post = post,
                Timestamp = DateTime.Now,
                User = user
            };

            _ctx.Comments.Add(newComment);
            await _ctx.SaveChangesAsync();

            var notification = await _notificationService.CreateNotification(post.User.UserName, User.Identity.Name, string.Format("{0} has commented on your post.", User.Identity.Name), _ctx);
            await _notificationService.SendNotification(notification, _ctx);

            return PartialView("~/Views/Post/_Comments.cshtml", post.Comments.ToList());
        }
    }
}