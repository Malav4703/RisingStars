using RisingStarsData.Entities;

namespace RisingStars.Models
{
    public class HomeViewModel
    {
        public StudentViewModel Student { get; set; }

        public List<Project> MyProjects { get; set; }


        public List<Announcement> Announcements { get; set; }

    }

}
