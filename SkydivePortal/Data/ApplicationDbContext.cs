using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SkydivePortal.Models;

namespace SkydivePortal.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUserRoles>()
            .HasKey(a => new { a.ApplicationRoleId, a.ApplicationUserId });

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }


        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Parachute> Parachutes { get; set; }
        public DbSet<UserJump> UserJumps { get; set; }
        public DbSet<Dropzone> Dropzones { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Dropzone_Post> Dropzone_Posts { get; set; }
        public DbSet<Dropzone_Post_Image> Dropzone_Post_Images { get; set; }
        public DbSet<Dropzone_Event> Dropzone_Events { get; set; }
        public DbSet<Dropzone_User_Post> Dropzone_User_Posts { get; set; }
        public DbSet<Dropzone_User_Post_Image> Dropzone_User_Post_Images { get; set; }
        public DbSet<Dropzone_User_Post_Comment> Dropzone_User_Post_Comments { get; set; }
        public DbSet<PagePost> PagePosts { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRoles> ApplicationUserRoles { get; set; }

    }
}
