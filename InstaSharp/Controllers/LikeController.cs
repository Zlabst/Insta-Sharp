using InstaSharp.Data.Context;
using InstaSharp.Data.Models;
using InstaSharp.Services;
using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InstaSharp.Controllers
{
    [Authorize]
    public class LikeController : Controller
    {
        private readonly InstaDbContext _ctx = new InstaDbContext();
        private readonly INotificationService _notificationService;

        public LikeController(INotificationService _notificationService)
        {
            this._notificationService = _notificationService;
        }

        [HttpPost]
        public async Task<string> Toggle(int postId)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);
            var post = await _ctx.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            var like = await _ctx.Likes.FirstOrDefaultAsync(l => l.Post.Id == postId && l.User.UserName == User.Identity.Name);

            if (like == null)
            {
                // Not yet liked, add new like
                var newLike = new Like
                {
                    Post = post,
                    Timestamp = DateTime.Now,
                    User = user
                };

                _ctx.Likes.Add(newLike);

                var notification = await _notificationService.CreateNotification(post.User.UserName, User.Identity.Name, string.Format("{0} has liked your post.", User.Identity.Name), _ctx);
                await _notificationService.SendNotification(notification, _ctx);
            }
            else
            {
                // Already liked post, unlike it
                _ctx.Likes.Remove(like);
            }

            await _ctx.SaveChangesAsync();

            return String.Empty;
        }
    }
}