using System.ComponentModel.DataAnnotations;

namespace RisingStarsData.Entities
{// Student Model
 // Skill Model 
    public class Skill
    {
        public int SkillId { get; set; }
        [Required] public string Name { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();

    }

}
