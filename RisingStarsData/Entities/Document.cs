using System.ComponentModel.DataAnnotations;

namespace RisingStarsData.Entities
{// Student Model
 // Document Model (project-related uploads)
    public class Document
    {
        public int DocumentId { get; set; }
        [Required] public string FileName { get; set; }
        public DateTime UploadDate { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }

}
