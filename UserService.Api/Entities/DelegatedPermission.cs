namespace UserService.Api.Entities
{
    public class DelegatedPermission
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
        public DateTime ExpiresAt { get; set; }

    }
}
