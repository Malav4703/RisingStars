using Microsoft.AspNetCore.Mvc;
using RisingStarsAdmin.Models;
using RisingStarsData.DataAccess;
using RisingStarsData.Entities;

namespace RisingStarsAdmin.Controllers;

public class AnnouncementController : Controller
{
    private readonly AppDbContext _context;

    public AnnouncementController(AppDbContext context)
    {
        _context = context;
    }


    // GET
    public IActionResult Index()
    {
        var announcements = _context.Announcements.ToList();

        return View(announcements);
    }

    public IActionResult Create()
    {
        var announcementViewModel = new AnnouncementViewModel();
        return View(announcementViewModel);
    }

    [HttpPost]
    public IActionResult Create(AnnouncementViewModel announcement)
    {
        if (ModelState.IsValid)
        {
            // Save the announcement to the database

            var newAnnouncement = new Announcement
            {
                Title = announcement.Title,
                Content = announcement.Description,
                PostDate = announcement.Date
            };

            _context.Announcements.Add(newAnnouncement);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(announcement);
    }

    public IActionResult Edit(int id)
    {
        var announcement = _context.Announcements.Find(id);
        var announcementViewModel = new AnnouncementViewModel
        {
            Title = announcement.Title,
            Description = announcement.Content,
            Date = announcement.PostDate
        };
        return View(announcementViewModel);
    }

    [HttpPost]
    public IActionResult Edit(AnnouncementViewModel announcement)
    {
        if (ModelState.IsValid)
        {
            var announcementToUpdate = _context.Announcements.Find(announcement.AnnouncementId);
            announcementToUpdate.Title = announcement.Title;
            announcementToUpdate.Content = announcement.Description;
            announcementToUpdate.PostDate = announcement.Date;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(announcement);
    }

    public IActionResult Details(int id)
    {
        var announcement = _context.Announcements.Find(id);
        var announcementViewModel = new AnnouncementViewModel
        {
            Title = announcement.Title,
            Description = announcement.Content,
            Date = announcement.PostDate
        };
        return View(announcementViewModel);
    }


    [HttpPost]
    public IActionResult Delete(int id)
    {
        var announcement = _context.Announcements.Find(id);
        _context.Announcements.Remove(announcement);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}