using SocialMediaApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMediaApp.ViewModels
{
    public class GroupCreateEditViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string? CoverImageUrl { get; set; }
        public long PrivacyStateId { get; set; }
        public List<PrivacyState> PrivacyStateList { get; set; }

    }
}
