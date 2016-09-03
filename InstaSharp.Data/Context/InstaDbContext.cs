using InstaSharp.Data.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace InstaSharp.Data.Context
{
    public class InstaDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Following> Following { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public InstaDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static InstaDbContext Create()
        {
            return new InstaDbContext();
        }
    }
}
