using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Review_service_WebAPI.Data;
using Review_service_WebAPI.Models;

namespace Review_service_WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookReviewsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BookReviewsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/BookReviews
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookReview>>> GetReviews()
    {
        try
        {
            var reviews = await _context.BookReviews.ToListAsync();
            return Ok(reviews);
        }
        catch (Exception ex)
        {
            // Logga gärna felet i verkliga projekt
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // GET: api/BookReviews/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookReview>> GetReview(int id)
    {
        var review = await _context.BookReviews.FindAsync(id);

        if (review == null)
        {
            return NotFound();
        }

        return review;
    }

    // POST: api/BookReviews
    [HttpPost]
    public async Task<ActionResult<BookReview>> CreateReview(BookReview review)
    {
        _context.BookReviews.Add(review);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReview), new { id = review.ReviewId }, review);
    }

    // PUT: api/BookReviews/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReview(int id, BookReview review)
    {
        if (id != review.ReviewId)
        {
            return BadRequest();
        }

        _context.Entry(review).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.BookReviews.Any(e => e.ReviewId == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/BookReviews/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        var review = await _context.BookReviews.FindAsync(id);

        if (review == null)
        {
            return NotFound();
        }

        _context.BookReviews.Remove(review);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}