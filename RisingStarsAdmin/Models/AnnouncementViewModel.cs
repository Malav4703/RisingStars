using System.ComponentModel.DataAnnotations;

namespace RisingStarsAdmin.Models;

public class AnnouncementViewModel
{
    public int? AnnouncementId { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Title")]
    public string Title { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name = "Description")]
    public string Description { get; set; }

    [Required]
    [Display(Name = "Date")]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; } = DateTime.Now.ToUniversalTime();
}