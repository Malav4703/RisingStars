using System.ComponentModel.DataAnnotations;

namespace RisingStarsData.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        [Required] public string Title { get; set; }
        [Required] public string Description { get; set; }

        public int TeamSize { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public List<Student> Members { get; set; } = new List<Student>();
        public List<Document> Documents { get; set; } = new List<Document>();
    }

}
