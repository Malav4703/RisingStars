using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RisingStarsData.Entities;

namespace RisingStarsData.DataAccess
{

    public class AppDbContext : IdentityDbContext<Student>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Student> Students { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Mentor> Mentors { get; set; }
        public DbSet<Mentorship> Mentorships { get; set; }
        public DbSet<Document> Documents { get; set; }

        public DbSet<Announcement> Announcements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Mentorship>()
                .HasKey(m => new { m.MentorId, m.StudentId });

            modelBuilder.Entity<Mentorship>()
                .HasOne(m => m.Mentor)
                .WithMany(m => m.Mentorships)
                .HasForeignKey(m => m.MentorId);

            modelBuilder.Entity<Mentorship>()
                .HasOne(m => m.Student)
                .WithMany(m => m.Mentees)
                .HasForeignKey(m => m.StudentId);

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Members)
                .WithMany(s => s.Projects)
                .UsingEntity(j => j.ToTable("ProjectMembers"));

            modelBuilder.Entity<Project>()
                .HasMany(p => p.Documents)
                .WithOne(d => d.Project)
                .HasForeignKey(d => d.ProjectId);

            modelBuilder.Entity<Mentor>()
                .HasMany(m => m.Mentees)
                .WithMany(s => s.Mentors)
                .UsingEntity(j => j.ToTable("MentorMentees"));

            modelBuilder.Entity<Student>()
                .HasMany(s => s.Skills)
                .WithMany(sk => sk.Students)
                .UsingEntity(j => j.ToTable("StudentSkills"));

            //seed announcements
            modelBuilder.Entity<Announcement>().HasData(
                new Announcement
                {
                    AnnouncementId = 1,
                    Title = "Welcome to Rising Stars!",
                    Content = "We are excited to have you join our community of students and mentors. We hope you find the resources and support you need to succeed in your academic and professional endeavors.",
                    PostDate = new DateTime(2024, 2, 12).ToUniversalTime()
                },
                new Announcement
                {
                    AnnouncementId = 2,
                    Title = "New Features",
                    Content = "We are constantly working to improve the Rising Stars platform. Keep an eye out for new features and updates!",
                    PostDate = new DateTime(2024, 3, 4).ToUniversalTime()
                }
                );


        }
    }

}
