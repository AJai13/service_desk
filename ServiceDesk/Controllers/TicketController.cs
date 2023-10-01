using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Data;
using ServiceDesk.Models;

namespace ServiceDesk;
[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase
{
    private ServiceDeskDbContext? _dbContext;

    public TicketController(ServiceDeskDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    [HttpPost]
    [Route("cadastrar")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CadastrarTicket([FromBody] Ticket ticket)
    {
        if (_dbContext is null)
        {
            return NotFound();
        }

        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            // Verifique se as propriedades relacionadas não são nulas e carregue-as do banco de dados
            if (ticket.status != null)
            {
                var status = await _dbContext.Status.FirstOrDefaultAsync(s => s.Id == ticket.status.Id);
                if (status == null)
                {
                    return BadRequest("Status não encontrado.");
                }
                ticket.status = status;
            }

            if (ticket.propriedade != null)
            {
                var propriedade = await _dbContext.Prioridade.FirstOrDefaultAsync(p => p.Id == ticket.propriedade.Id);
                if (propriedade == null)
                {
                    return BadRequest("Prioridade não encontrada.");
                }
                ticket.propriedade = propriedade;
            }

            // Repita o processo para outras propriedades relacionadas

            await _dbContext.AddAsync(ticket);
            await _dbContext.SaveChangesAsync();
            transaction.Commit();

            return Created("", ticket);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            return BadRequest($"Ocorreu um erro ao cadastrar o Ticket: {ex.Message}");
        }
    }



    [HttpGet]
    [Route("getTicket/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult GetTicket(int id)
    {
        if (_dbContext is null) return NotFound();

        var ticket = _dbContext.Ticket
            .Include(t => t.status)
            .Include(t => t.propriedade)
            .Include(t => t.categoria)
            .Include(t => t.sla)
            .Include(t => t.Dispositivo)
            .Include(t => t.usuarioCriador)
            .Include(t => t.funcionarioResponsavel)
            .FirstOrDefault(t => t.Id == id);

        if (ticket != null)
        {
            return Ok(ticket);
        }

        return NotFound("Ticket não encontrado.");
    }



}
