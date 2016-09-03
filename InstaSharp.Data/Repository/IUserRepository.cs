using InstaSharp.Data.Context;
using InstaSharp.Data.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace InstaSharp.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<ApplicationUser> GetByUsername(string userName, InstaDbContext _ctx)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<ApplicationUser> GetByUsernameOrId(string userName, InstaDbContext _ctx)
        {
            return await _ctx.Users.FirstOrDefaultAsync(u => u.UserName == userName || u.Id == userName);
        }
    }

    public interface IUserRepository
    {
        Task<ApplicationUser> GetByUsername(string userName, InstaDbContext _ctx);
        Task<ApplicationUser> GetByUsernameOrId(string userName, InstaDbContext _ctx);
    }
}
