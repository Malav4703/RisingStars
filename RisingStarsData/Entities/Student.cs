using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RisingStarsData.Entities
{// Student Model
    public class Student : IdentityUser
    {
        public string StudentId { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }

        [Required] public string Email { get; set; }

        [Required]
        public DateOnly BirthDate { get; set; }

        public string? Bio { get; set; }

        [Required]
        public string Major { get; set; }

        [Required]
        public string School { get; set; }

        [Required]
        public DateOnly JoinDate { get; set; }

        public List<Mentor> Mentors { get; set; } = new List<Mentor>();

        public List<Project> Projects { get; set; } = new List<Project>();
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public List<Mentorship> Mentees { get; set; } = new List<Mentorship>();
    }

}
