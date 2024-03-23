using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RisingStars.Models;
using RisingStarsData.DataAccess;
using RisingStarsData.Entities;
using System.Diagnostics;

namespace RisingStars.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Student> _userManager;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, UserManager<Student> userManager, AppDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // top 3 announcements
            var announcements = await _context.Announcements
                .OrderByDescending(a => a.PostDate)
                .Take(3)
                .ToListAsync();

            // my projects
            var projects = await _context.Projects
                .Include(p => p.Members)
                .Where(p => p.Members.Any(m => m.StudentId == user.Id))
                .ToListAsync();

            var studentViewModel = new StudentViewModel
            {
                StudentId = user.StudentId,
                FullName = user.FirstName + " " + user.LastName,
                Major = user.Major,
                Email = user.Email,
            };

            var homeViewModel = new HomeViewModel
            {
                Announcements = announcements,
                MyProjects = projects,
                Student = studentViewModel
            };



            return View(homeViewModel);
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
