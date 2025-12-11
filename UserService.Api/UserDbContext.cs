using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Claims;
using UserService.Api.Entities;

namespace UserService.Api
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserPermission>().HasKey(x=> new { x.UserId, x.PermissionId });

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
    }
}
