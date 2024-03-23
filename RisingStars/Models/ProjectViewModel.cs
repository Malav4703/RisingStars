using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace RisingStars.Models
{
    public class ProjectViewModel
    {
        // Project Details
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddMonths(1);

        // List of Students to choose from for assigning to the project
        public List<StudentViewModel> AvailableStudents { get; set; } = new List<StudentViewModel>();

        // IDs of the students selected to be assigned to the project
        [Required]
        public List<string> SelectedStudentIds { get; set; } = new List<string>();

        [ValidateNever]
        public string CreatorStudentId { get; set; }
    }

}
