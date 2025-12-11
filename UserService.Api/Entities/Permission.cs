using Authorization;

namespace UserService.Api.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public UserClaimEnum UserClaimEnum { get; set; }
        public string Name { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}
