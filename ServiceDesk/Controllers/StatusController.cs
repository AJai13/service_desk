using Microsoft.AspNetCore.Mvc;
using ServiceDesk;
using ServiceDesk.Data;
using ServiceDesk.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class StatusController : ControllerBase
{
    private ServiceDeskDbContext? _dbContext;

    public StatusController(ServiceDeskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("getStatus/{id}")]
    public IActionResult GetStatus(int id)
    {
        var sla = _dbContext.Status.FirstOrDefault(s => s.Id == id);

        if (sla == null)
        {
            return NotFound("Status não encontrada.");
        }

        return Ok(sla);
    }


    [HttpPost]
    [Route("setStatus")]
    public async Task<ActionResult> SetStatus(Status status)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Status is null) return NotFound();
        await _dbContext.AddAsync(status);
        await _dbContext.SaveChangesAsync();
        return Created("", status);
    }


    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Status>>> Listar()
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Status is null) return NotFound();
        return await _dbContext.Status.ToListAsync();
    }


    [HttpPost]
    [Route("associarStatusAoTicket/{ticketId}/{statusId}")]
    public IActionResult AssociarStatusAoTicket(int ticketId, int statusId)
    {
        try
        {
            // Verifique se o ticket com o ticketId existe no banco de dados
            var ticket = _dbContext.Ticket.FirstOrDefault(t => t.Id == ticketId);
            if (ticket == null)
            {
                return NotFound("O ticket especificado não foi encontrado.");
            }

            // Verifique se o status com o statusId existe no banco de dados
            var status = _dbContext.Status.FirstOrDefault(s => s.Id == statusId);
            if (status == null)
            {
                return NotFound("O status especificado não foi encontrado.");
            }

            // Associe o status ao ticket
            ticket.status = status;

            _dbContext.SaveChanges();

            return Ok($"Status associado ao ticket '{ticket.Titulo}' com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }
    
    [HttpPut]
    [Route("alterar/{id}")]
    public async Task<ActionResult> Alterar(int id, [FromBody] Status status)
    {
        try
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            // Verifique se o status com o antigoNome especificado existe
            var statusExistente = await _dbContext.Status.FirstOrDefaultAsync(d => d.Id == id);

            if (statusExistente == null)
            {
                return NotFound("Status não encontrado.");
            }

            statusExistente.Nome = status.Nome;

            _dbContext.Status.Update(statusExistente);
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
        if (_dbContext.Status is null) return NotFound();
        var statusTmp = await _dbContext.Status.FindAsync(id);
        if (statusTmp is null) return NotFound();
        _dbContext.Remove(statusTmp);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

}