using InstaSharp.Data.Context;
using InstaSharp.Data.Models;
using InstaSharp.Data.Repository;
using System.Threading.Tasks;

namespace InstaSharp.Services
{
    public class UserService : IUserService
    {
        public IUserRepository _userRepository { get; set; }

        public UserService(IUserRepository _userRepository)
        {
            this._userRepository = _userRepository;
        }

        public async Task<ApplicationUser> GetByUsername(string userName, InstaDbContext _ctx)
        {
            return await _userRepository.GetByUsername(userName, _ctx);
        }

        public async Task<ApplicationUser> GetByUsernameOrId(string userName, InstaDbContext _ctx)
        {
            return await _userRepository.GetByUsernameOrId(userName, _ctx);
        }
    }

    public interface IUserService
    {
        Task<ApplicationUser> GetByUsername(string userName, InstaDbContext _ctx);
        Task<ApplicationUser> GetByUsernameOrId(string userName, InstaDbContext _ctx);
    }
}