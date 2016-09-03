using InstaSharp.Controllers;
using InstaSharp.Data.Repository;
using InstaSharp.Services;
using Microsoft.Practices.Unity;
using System.Web.Mvc;
using Unity.Mvc3;

namespace InstaSharp
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<AccountController>(new InjectionConstructor());

            // Repositories
            container.RegisterType<IUserRepository, UserRepository>();

            // Services
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IFollowService, FollowService>();
            container.RegisterType<INotificationService, NotificationService>();

            return container;
        }
    }
}