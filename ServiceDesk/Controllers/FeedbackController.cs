using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Data;

[ApiController]
[Route("api/[controller]")]
public class FeedbackController : ControllerBase
{
    private readonly ServiceDeskDbContext _dbContext;

    public FeedbackController(ServiceDeskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("getFeedback")]
    public IActionResult GetFeedback()
    {
        var feedbacks = _dbContext.Feedback.Include(f => f.Solucao).ToList();
        return Ok(feedbacks);
    }

    [HttpPost("setFeedback")]
    public IActionResult SetFeedback([FromBody] Feedback feedback)
    {
        if (feedback == null || string.IsNullOrWhiteSpace(feedback.FeedbackText) || feedback.SolucaoId <= 0)
        {
            return BadRequest("O feedback e uma Solucao válida são obrigatórios.");
        }

        _dbContext.Feedback.Add(feedback);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetFeedback), new { id = feedback.Id }, feedback);
    }
}
