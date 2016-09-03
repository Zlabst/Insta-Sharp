using InstaSharp.Data.Context;
using InstaSharp.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace InstaSharp.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUserService _userService;

        public NotificationService(IUserService _userService)
        {
            this._userService = _userService;
        }

        /// <summary>
        /// Create a notification to be sent.
        /// </summary>
        /// <param name="toUser"></param>
        /// <param name="fromUser"></param>
        /// <param name="notificationMessage"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task<Notification> CreateNotification(string toUser, string fromUser, string notificationMessage, InstaDbContext _ctx)
        {
            var notification = new Notification
            {
                ToUser = await _userService.GetByUsername(toUser, _ctx),
                FromUser = await _userService.GetByUsername(fromUser, _ctx),
                Message = notificationMessage,
                Viewed = false,
                Timestamp = DateTime.Now
            };

            return notification;
        }

        /// <summary>
        /// Send the notification.
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task SendNotification(Notification notification, InstaDbContext _ctx)
        {
            _ctx.Notifications.Add(notification);
            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Mark a user's notifications as viewed.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public async Task MarkNotificationsViewed(string userName, InstaDbContext _ctx)
        {
            var notifications = _ctx.Notifications.Where(n => n.ToUser.UserName == userName).ToList();
            notifications.ForEach(n => n.Viewed = true);

            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Get the provided user's notifications.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="_ctx"></param>
        /// <returns></returns>
        public Task<List<Notification>> GetNotifications(string userName, InstaDbContext _ctx, bool includeViewed = false)
        {
            var notifications = _ctx.Notifications.Where(n => n.ToUser.UserName == userName /*&&
                n.Viewed == includeViewed ? n.Viewed : false*/);
            return notifications.ToListAsync();
        }
    }

    public interface INotificationService
    {
        Task<Notification> CreateNotification(string toUser, string fromUser, string notificationMessage, InstaDbContext _ctx);
        Task SendNotification(Notification notification, InstaDbContext _ctx);
        Task MarkNotificationsViewed(string userName, InstaDbContext _ctx);
        Task<List<Notification>> GetNotifications(string userName, InstaDbContext _ctx, bool includeViewed = false);
    }
}