using Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Claims;
using UserService.Api.Entities;
using Utils;

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
            modelBuilder.Entity<Permission>().HasData(
                new Permission() { Id = ((int)UserClaimEnum.StandardUser), 
                    Name = UserClaimEnum.StandardUser.ToString(), UserClaimEnum = UserClaimEnum.StandardUser},
                new Permission() { Id = ((int)UserClaimEnum.Admin), Name = UserClaimEnum.Admin.ToString(), UserClaimEnum = UserClaimEnum.Admin}
                );
            modelBuilder.Entity<User>().HasData(new User() { Id = 1 ,Email= "admin@email.com", Name = "admin", Password = CryptoUtils.Hash("admin")});
            modelBuilder.Entity<UserPermission>().HasData(new UserPermission() { PermissionId  = 2, UserId = 1});
            modelBuilder.Entity<Permission>().HasIndex(x=> new {x.Id,x.UserClaimEnum}).IsUnique();

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
    }
}
