using System.ComponentModel.DataAnnotations;

namespace RisingStarsData.Entities
{// Student Model
    public class Announcement
    {
        public int AnnouncementId { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Content { get; set; }
        public DateTime PostDate { get; set; }
    }

}
