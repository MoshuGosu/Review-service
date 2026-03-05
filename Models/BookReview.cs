using System.ComponentModel.DataAnnotations;

namespace Review_service_WebAPI.Models;

public class BookReview
{
    [Key]
    public int ReviewId { get; set; }

    [Required]
    public string BookTitle { get; set; }

    [Required]
    public string ReviewerName { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    public string? Text { get; set; }

    public DateTime Date { get; set; } = DateTime.UtcNow;
}