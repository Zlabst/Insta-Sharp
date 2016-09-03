using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InstaSharp.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Your name")]
        public string RealName { get; set; }

        [StringLength(150, ErrorMessage = "Your bio must be less than 150 characters.")]
        public string Bio { get; set; }

        [Display(Name = "Profile photo")]
        public string Photo { get; set; }

        public virtual List<Post> Posts { get; set; }

        public bool Private { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string GetPhoto()
        {
            return String.IsNullOrEmpty(Photo) ? "Default_Profile.jpg" : Photo;
        }
    }
}