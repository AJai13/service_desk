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


    [HttpGet("getFeedback/{id}")]
    public async Task<ActionResult<Feedback>> GetFeedback(int id)
    {
        var feedback = await _dbContext.Feedback.FindAsync(id);
        if (feedback == null)
        {
            return NotFound();
        }

        return feedback;
    }


    [HttpPost("setFeedbackAssociarASolucao")]
    public IActionResult SetFeedback([FromBody] Feedback feedback)
    {
        if (feedback == null || string.IsNullOrWhiteSpace(feedback.FeedbackText) || feedback.SolucaoId <= 0)
        {
            return BadRequest("O feedback e uma Solucao válida são obrigatórios.");
        }

        // Certifique-se de que a Solucao com o solucaoId existe no banco de dados
        var solucao = _dbContext.Solucao.FirstOrDefault(s => s.Id == feedback.SolucaoId);
        if (solucao == null)
        {
            return BadRequest("A Solucao especificada não foi encontrada.");
        }

        // Associe o Feedback à Solucao
        feedback.Solucao = solucao;

        _dbContext.Feedback.Add(feedback);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetFeedback), new { id = feedback.Id }, feedback);
    }

}
