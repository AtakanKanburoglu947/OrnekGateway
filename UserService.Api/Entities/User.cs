using System.ComponentModel.DataAnnotations;

namespace UserService.Api.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
        public ICollection<DelegatedPermission> DelegatedPermissions { get; set; }

    }
}
