using Microsoft.AspNetCore.Mvc;
using ServiceDesk;
using ServiceDesk.Data;
using ServiceDesk.Models;

[ApiController]
[Route("[controller]")]
public class SLAController : ControllerBase
{
    private ServiceDeskDbContext? _dbContext;

    public SLAController(ServiceDeskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("getSla/{id}")]
    public IActionResult GetSla(int id)
    {
        var sla = _dbContext.Sla.FirstOrDefault(s => s.Id == id);

        if (sla == null)
        {
            return NotFound("Solução não encontrada.");
        }

        return Ok(sla);
    }


    [HttpPost]
    [Route("setSla")]
    public async Task<ActionResult> SetSla(Sla sla)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Sla is null) return NotFound();
        await _dbContext.AddAsync(sla);
        await _dbContext.SaveChangesAsync();
        return Created("", sla);
    }


    [HttpPost]
    [Route("adicionarTicket")]
    public async Task<ActionResult> AdicionarTicket(int ticketId)
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Ticket is null) return NotFound();

        // Recupere o Ticket pelo ID
        var ticket = await _dbContext.Ticket.FindAsync(ticketId);

        // Verifique se o Ticket existe
        if (ticket == null)
        {
            return BadRequest("Ticket não encontrado.");
        }

        // Adicione o Ticket ao SLA com base nas regras internas do sistema
        // Você deve implementar essa lógica com base em como os SLAs são atribuídos no seu sistema

        try
        {
            // Determine o SLA com base nas regras internas do sistema e atribua-o ao ticket
            // ticket.Sla = ...; // Atribua o SLA ao ticket

            await _dbContext.SaveChangesAsync();
            return Created("", ticket);
        }
        catch (Exception ex)
        {
            return BadRequest($"Ocorreu um erro ao adicionar o Ticket ao SLA: {ex.Message}");
        }
    }
}
