using Microsoft.AspNetCore.Mvc;
using ServiceDesk;
using ServiceDesk.Data;
using ServiceDesk.Models;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("[controller]")]
public class SLAController : ControllerBase
{
    private ServiceDeskDbContext _dbContext;

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

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Sla>>> Listar()
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Sla is null) return NotFound();
        return await _dbContext.Sla.ToListAsync();
    }


    [HttpPost]
    [Route("associarSlaAoTicket/{ticketId}/{slaId}")]
    public IActionResult AssociarSlaAoTicket(int ticketId, int slaId)
    {
        try
        {
            // Verifique se o ticket com o ticketId existe no banco de dados
            var ticket = _dbContext.Ticket.FirstOrDefault(t => t.Id == ticketId);
            if (ticket == null)
            {
                return NotFound("O ticket especificado não foi encontrado.");
            }

            // Verifique se o SLA com o slaId existe no banco de dados
            var sla = _dbContext.Sla.FirstOrDefault(s => s.Id == slaId);
            if (sla == null)
            {
                return NotFound("O SLA especificado não foi encontrado.");
            }

            // Associe o SLA ao ticket
            ticket.sla = sla;

            _dbContext.SaveChanges();

            return Ok($"SLA associado ao ticket '{ticket.Titulo}' com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpPut]
    [Route("alterar/{id}")]
    public async Task<ActionResult> Alterar(int id, [FromBody] Sla sla)
    {
        try
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            // Verifique se o sla com o antigoNome especificado existe
            var slaExistente = await _dbContext.Sla.FirstOrDefaultAsync(d => d.Id == id);

            if (slaExistente == null)
            {
                return NotFound("Sla não encontrado.");
            }

            slaExistente.Nome = sla.Nome;

            _dbContext.Sla.Update(slaExistente);
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
        if (_dbContext.Sla is null) return NotFound();
        var slaTmp = await _dbContext.Sla.FindAsync(id);
        if (slaTmp is null) return NotFound();
        _dbContext.Remove(slaTmp);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }

}
