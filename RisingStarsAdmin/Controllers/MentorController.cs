using Microsoft.AspNetCore.Mvc;
using RisingStarsAdmin.Models;
using RisingStarsData.DataAccess;
using RisingStarsData.Entities;

namespace RisingStarsAdmin.Controllers
{

    public class MentorController : Controller
    {

        private readonly AppDbContext _context;

        public MentorController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var mentors = _context.Mentors;

            var mentorViewModels = new List<MentorViewModel>();

            foreach (var mentor in mentors)
            {
                var mentorViewModel = new MentorViewModel
                {
                    MentorId = mentor.MentorId,
                    FirstName = mentor.FirstName,
                    LastName = mentor.LastName,
                    Email = mentor.Email,
                    PhoneNumber = mentor.PhoneNumber,
                    Bio = mentor.Bio,
                    ProfilePicture = mentor.ProfilePicture
                };

                mentorViewModels.Add(mentorViewModel);
            }

            return View(mentorViewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MentorViewModel mentor)
        {
            if (ModelState.IsValid)
            {
                var newMentor = new Mentor
                {
                    FirstName = mentor.FirstName,
                    LastName = mentor.LastName,
                    Email = mentor.Email,
                    PhoneNumber = mentor.PhoneNumber,
                    Bio = mentor.Bio,
                    ProfilePicture = mentor.ProfilePicture
                };

                _context.Mentors.Add(newMentor);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(mentor);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var mentor = _context.Mentors.Find(id);

            if (mentor == null)
            {
                return RedirectToAction("Index");
            }

            var mentorViewModel = new MentorViewModel
            {
                MentorId = mentor.MentorId,
                FirstName = mentor.FirstName,
                LastName = mentor.LastName,
                Email = mentor.Email,
                PhoneNumber = mentor.PhoneNumber,
                Bio = mentor.Bio,
                ProfilePicture = mentor.ProfilePicture
            };

            return View(mentorViewModel);
        }

        [HttpPost]
        public IActionResult Edit(MentorViewModel mentor)
        {
            if (ModelState.IsValid)
            {
                var existingMentor = _context.Mentors.Find(mentor.MentorId);

                if (existingMentor == null)
                {
                    return RedirectToAction("Index");
                }

                existingMentor.FirstName = mentor.FirstName;
                existingMentor.LastName = mentor.LastName;
                existingMentor.Email = mentor.Email;
                existingMentor.PhoneNumber = mentor.PhoneNumber;
                existingMentor.Bio = mentor.Bio;
                existingMentor.ProfilePicture = mentor.ProfilePicture;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(mentor);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var mentor = _context.Mentors.Find(id);

            if (mentor == null)
            {
                return RedirectToAction("Index");
            }

            var mentorViewModel = new MentorViewModel
            {
                MentorId = mentor.MentorId,
                FirstName = mentor.FirstName,
                LastName = mentor.LastName,
                Email = mentor.Email,
                PhoneNumber = mentor.PhoneNumber,
                Bio = mentor.Bio,
                ProfilePicture = mentor.ProfilePicture
            };

            return View(mentorViewModel);
        }

        [HttpPost]
        public IActionResult Delete(MentorViewModel mentor)
        {
            var existingMentor = _context.Mentors.Find(mentor.MentorId);

            if (existingMentor == null)
            {
                return RedirectToAction("Index");
            }

            _context.Mentors.Remove(existingMentor);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var mentor = _context.Mentors.Find(id);

            if (mentor == null)
            {
                return RedirectToAction("Index");
            }

            var mentorViewModel = new MentorViewModel
            {
                MentorId = mentor.MentorId,
                FirstName = mentor.FirstName,
                LastName = mentor.LastName,
                Email = mentor.Email,
                PhoneNumber = mentor.PhoneNumber,
                Bio = mentor.Bio,
                ProfilePicture = mentor.ProfilePicture
            };

            return View(mentorViewModel);
        }

    }
}
