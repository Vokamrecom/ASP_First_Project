using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication3.Models;

namespace WebApplication3.Models
{
    public class AppUserManager : UserManager<ApplicationUser>
    {
        public AppUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        { }

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options,
            IOwinContext context)
        {
            ApplicationDbContext db = context.Get<ApplicationDbContext>();
            AppUserManager manager = new AppUserManager(new UserStore<ApplicationUser>(db));
            return manager;
        }

    }

    public class CreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}