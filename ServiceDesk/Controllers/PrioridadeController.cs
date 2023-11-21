using Microsoft.AspNetCore.Mvc;
using ServiceDesk;
using ServiceDesk.Data;
using ServiceDesk.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class PrioridadeController : ControllerBase
{
    private ServiceDeskDbContext _dbContext;

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
            ticket.prioridade = prioridade;

            _dbContext.SaveChanges();

            return Ok($"Prioridade associada ao ticket '{ticket.Titulo}' com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Prioridade>>> Listar()
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Prioridade is null) return NotFound();
        return await _dbContext.Prioridade.ToListAsync();
    }

    [HttpPut]
    [Route("alterar/{id}")]
    public async Task<ActionResult> Alterar(int id, [FromBody] Prioridade prioridade)
    {
        try
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            // Verifique se o prioridade com o antigoNome especificado existe
            var prioridadeExistente = await _dbContext.Prioridade.FirstOrDefaultAsync(d => d.Id == id);

            if (prioridadeExistente == null)
            {
                return NotFound("Prioridade não encontrado.");
            }

            prioridadeExistente.Nome = prioridade.Nome;

            _dbContext.Prioridade.Update(prioridadeExistente);
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
        if (_dbContext.Prioridade is null) return NotFound();
        var prioridadeTmp = await _dbContext.Prioridade.FindAsync(id);
        if (prioridadeTmp is null) return NotFound();
        _dbContext.Remove(prioridadeTmp);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
