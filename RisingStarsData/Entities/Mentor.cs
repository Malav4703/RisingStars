using System.ComponentModel.DataAnnotations;

namespace RisingStarsData.Entities
{// Student Model
    public class Mentor
    {
        [Key]
        public int MentorId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


        public string Bio { get; set; }


        public string ProfilePicture { get; set; }

        public List<Student> Mentees { get; set; } = new List<Student>();

        public List<Skill> Skills { get; set; } = new List<Skill>();
        public List<Mentorship> Mentorships { get; set; } = new List<Mentorship>();
    }

}
