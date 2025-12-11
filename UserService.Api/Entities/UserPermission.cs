namespace UserService.Api.Entities
{
    public class UserPermission
    {
        public User User { get; set; }
        public int UserId { get; set; }
        public Permission Permission { get; set; }
        public int PermissionId { get; set; }
    }
}
