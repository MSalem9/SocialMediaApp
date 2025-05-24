namespace SocialMediaApp.ViewModels
{
    public class GroupInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public long GroupAdminId { get; set; }
    }
}
