namespace RisingStarsData.Entities
{// Student Model
 // Mentorship Model (for many-to-many since a student can have multiple mentors, and vice-versa)
    public class Mentorship
    {
        public int MentorshipId { get; set; }

        public string StudentId { get; set; }
        public Student Student { get; set; }

        public int MentorId { get; set; }
        public Mentor Mentor { get; set; }
    }

}
