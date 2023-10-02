using Microsoft.AspNetCore.Mvc;
using ServiceDesk;
using ServiceDesk.Data;
using ServiceDesk.Models;

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


}