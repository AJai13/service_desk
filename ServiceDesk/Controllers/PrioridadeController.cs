using Microsoft.AspNetCore.Mvc;
using ServiceDesk;
using ServiceDesk.Data;
using ServiceDesk.Models;

[ApiController]
[Route("[controller]")]
public class PrioridadeController : ControllerBase
{
    private ServiceDeskDbContext? _dbContext;

    public PrioridadeController(ServiceDeskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("getPrioridade/{id}")]
    public IActionResult GetPrioridade(int id)
    {
        var prioridade = _dbContext.Prioridade.FirstOrDefault(s => s.Id == id);

        if (prioridade == null)
        {
            return NotFound("Solução não encontrada.");
        }

        return Ok(prioridade);
    }

    [HttpPost]
    [Route("setPrioridade")]
    public async Task<ActionResult> SetPrioridade(Prioridade prioridade)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Prioridade is null) return NotFound();
        await _dbContext.AddAsync(prioridade);
        await _dbContext.SaveChangesAsync();
        return Created("", prioridade);
    }

    [HttpPost]
    [Route("associarPrioridadeAoTicket/{ticketId}/{prioridadeId}")]
    public IActionResult AssociarPrioridadeAoTicket(int ticketId, int prioridadeId)
    {
        try
        {
            // Verifique se o ticket com o ticketId existe no banco de dados
            var ticket = _dbContext.Ticket.FirstOrDefault(t => t.Id == ticketId);
            if (ticket == null)
            {
                return NotFound("O ticket especificado não foi encontrado.");
            }

            // Verifique se a prioridade com o prioridadeId existe no banco de dados
            var prioridade = _dbContext.Prioridade.FirstOrDefault(p => p.Id == prioridadeId);
            if (prioridade == null)
            {
                return NotFound("A prioridade especificada não foi encontrada.");
            }

            // Associe a prioridade ao ticket
            ticket.propriedade = prioridade;

            _dbContext.SaveChanges();

            return Ok($"Prioridade associada ao ticket '{ticket.Titulo}' com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }



}
