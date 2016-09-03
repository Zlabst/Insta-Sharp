using InstaSharp.Data.Context;
using InstaSharp.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InstaSharp.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        private readonly InstaDbContext _ctx = new InstaDbContext();
        private readonly IUserService _userService;
        private readonly IFollowService _followService;
        private readonly INotificationService _notificationService;

        public FollowController(IUserService _userService,
                                IFollowService _followService,
                                INotificationService _notificationService)
        {
            this._userService = _userService;
            this._followService = _followService;
            this._notificationService = _notificationService;
        }

        public async Task<ActionResult> Follow(string userName)
        {
            await _followService.Follow(User.Identity.Name, userName, _ctx);

            var notification = await _notificationService.CreateNotification(userName, User.Identity.Name, string.Format("{0} has followed you.", User.Identity.Name), _ctx);
            await _notificationService.SendNotification(notification, _ctx);

            return RedirectToAction("Index", "Profile", new { id = userName });
        }

        public async Task<ActionResult> Unfollow(string userName)
        {
            await _followService.Unfollow(User.Identity.Name, userName, _ctx);
            return RedirectToAction("Index", "Profile", new { id = userName });
        }
    }
}