using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RisingStars.Models;
using RisingStarsData.DataAccess;
using RisingStarsData.Entities;

namespace RisingStars.Controllers
{
    public class ProjectController : Controller
    {

        private readonly AppDbContext _context;
        private readonly UserManager<Student> _userManager;

        public ProjectController(AppDbContext context, UserManager<Student> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Show All Projects
        public IActionResult Index()
        {
            var projects = _context.Projects;
            return View(projects);
        }

        // Show my Projects
        public async Task<IActionResult> MyProjects()
        {

            var user = await _userManager.GetUserAsync(User);

            var projects = _context.Projects
                .Where(p => p.Members.Any(m => m.StudentId == user.StudentId))
                .ToList();

            return View(projects);
        }

        // Show Project Details
        public IActionResult Details(int id)
        {
            var project = _context.Projects
                .Include(p => p.Members)
                .FirstOrDefault(p => p.ProjectId == id);
            return View(project);
        }


        // Create a new Project
        public async Task<IActionResult> CreateProject()
        {
            var students = _context.Students.Select(s => new StudentViewModel
            {
                StudentId = s.StudentId,
                FullName = $"{s.FirstName} {s.LastName}"
            }).ToList();

            var user = await _userManager.GetUserAsync(User);


            var model = new ProjectViewModel
            {
                AvailableStudents = students,
                CreatorStudentId = user.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new project from the ViewModel
                var project = new Project
                {
                    Title = model.Title,
                    Description = model.Description,
                    TeamSize = model.SelectedStudentIds.Count + 1,
                    StartDate = model.StartDate.ToUniversalTime(),
                    EndDate = model.EndDate.ToUniversalTime(),
                    Members = new List<Student>()
                };

                // Retrieve the creator student from the database
                var creatorStudent = await _userManager.FindByIdAsync(model.CreatorStudentId);
                if (creatorStudent != null)
                {
                    project.Members.Add(creatorStudent);
                }
                else
                {
                    // Handle error: Creator student not found
                    ModelState.AddModelError("", "The project creator was not found.");
                    return View(model); // Assuming a view that takes this ViewModel
                }

                // Assign selected students to the project
                foreach (var studentId in model.SelectedStudentIds)
                {
                    var student = await _context.Users.FindAsync(studentId);
                    if (student != null)
                    {
                        project.Members.Add(student);
                    }
                    // Optionally handle the case where a student isn't found
                }

                // Save the project to the database
                _context.Add(project);
                await _context.SaveChangesAsync();

                // Redirect to a confirmation page or the project details page
                return RedirectToAction("Details", new { id = project.ProjectId });
            }

            // If we got this far, something failed, redisplay form
            return View(model); // Assuming a view that takes this ViewModel
        }

        // Edit Project
        public IActionResult Edit(int id)
        {
            var project = _context.Projects
                .Include(p => p.Members)
                .FirstOrDefault(p => p.ProjectId == id);

            var students = _context.Students.Select(s => new StudentViewModel
            {
                StudentId = s.StudentId,
                FullName = $"{s.FirstName} {s.LastName}"
            }).ToList();

            var model = new ProjectViewModel
            {
                Title = project.Title,
                Description = project.Description,
                StartDate = project.StartDate.ToUniversalTime(),
                EndDate = project.EndDate.ToUniversalTime(),
                CreatorStudentId = project.Members.FirstOrDefault()?.Id,
                AvailableStudents = students
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = _context.Projects.Find(id);
                if (project == null)
                {
                    return NotFound();
                }

                project.Title = model.Title;
                project.Description = model.Description;
                project.TeamSize = model.SelectedStudentIds.Count + 1;
                project.StartDate = model.StartDate.ToUniversalTime();
                project.EndDate = model.EndDate.ToUniversalTime();

                // Retrieve the creator student from the database
                var creatorStudent = await _userManager.FindByIdAsync(model.CreatorStudentId);
                if (creatorStudent != null)
                {
                    project.Members.Clear();
                    project.Members.Add(creatorStudent);
                }
                else
                {
                    // Handle error: Creator student not found
                    ModelState.AddModelError("", "The project creator was not found.");
                    return View(model); // Assuming a view that takes this ViewModel
                }
                var memberIds = project.Members.Select(m => m.StudentId).ToList();
                foreach (var memberId in memberIds)
                {
                    if (!model.SelectedStudentIds.Contains(memberId))
                    {
                        var memberToRemove = project.Members.FirstOrDefault(m => m.StudentId == memberId);
                        if (memberToRemove != null)
                        {
                            project.Members.Remove(memberToRemove);
                        }
                    }
                }

                foreach (var studentId in model.SelectedStudentIds)
                {
                    if (!project.Members.Any(m => m.StudentId == studentId))
                    {
                        var studentToAdd = await _context.Users.FindAsync(studentId);
                        if (studentToAdd != null)
                        {
                            project.Members.Add(studentToAdd);
                        }
                    }
                }

                // Now proceed with the update
                _context.Update(project);
                await _context.SaveChangesAsync();

                // Redirect to a confirmation page or the project details page
                return RedirectToAction("Details", new { id = project.ProjectId });
            }

            // If we got this far, something failed, redisplay form
            return View(model); // Assuming a view that takes this ViewModel
        }

        // Delete Project
        public IActionResult Delete(int id)
        {
            var project = _context.Projects.Find(id);
            return View(project);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = _context.Projects.Find(id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }




    }

}
