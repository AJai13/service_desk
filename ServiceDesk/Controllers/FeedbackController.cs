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

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Feedback>>> Listar()
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Feedback is null) return NotFound();
        return await _dbContext.Feedback.ToListAsync();
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

    [HttpPut]
    [Route("alterar/{id}")]
    public async Task<ActionResult> Alterar(int id, [FromBody] Feedback feedback)
    {
        try
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            // Verifique se o feedback com o antigoNome especificado existe
            var feedbackExistente = await _dbContext.Feedback.FirstOrDefaultAsync(d => d.Id == id);

            if (feedbackExistente == null)
            {
                return NotFound("Feedback não encontrado.");
            }

            feedbackExistente.FeedbackText = feedback.FeedbackText;
            feedbackExistente.SolucaoId = feedback.SolucaoId;

            _dbContext.Feedback.Update(feedbackExistente);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpDelete]
    [Route("excluir/{id}")]
    public async Task<ActionResult> Excluir(int id)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Feedback is null) return NotFound();
        var feedbackTemp = await _dbContext.Feedback.FindAsync(id);
        if (feedbackTemp is null) return NotFound();
        _dbContext.Remove(feedbackTemp);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

}
