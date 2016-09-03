using InstaSharp.Data.Context;
using InstaSharp.Services;
using System.Web.Mvc;

namespace InstaSharp.Controllers
{
    [Authorize]
    public class NotificationController : Controller
    {
        private readonly InstaDbContext _ctx = new InstaDbContext();
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService _notificationService)
        {
            this._notificationService = _notificationService;
        }

        public int Count()
        {
            var notifications = _notificationService.GetNotifications(User.Identity.Name, _ctx);
            return notifications.Result.Count;
        }
    }
}