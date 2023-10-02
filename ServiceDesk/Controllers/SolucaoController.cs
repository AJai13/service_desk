using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk;
using ServiceDesk.Data; // Importe o contexto do banco de dados

[ApiController]
[Route("api/[controller]")]
public class SolucaoController : ControllerBase
{
    private readonly ServiceDeskDbContext _dbContext;

    public SolucaoController(ServiceDeskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("getSolucao/{id}")]
    public IActionResult GetSolucao(int id)
    {
        var solucao = _dbContext.Solucao.FirstOrDefault(s => s.Id == id);

        if (solucao == null)
        {
            return NotFound("Solução não encontrada.");
        }

        return Ok(solucao);
    }


    [HttpPost("setSolucao")]
    public IActionResult SetSolucao([FromBody] Solucao solucao)
    {
        if (solucao == null)
        {
            return BadRequest("Os dados da solução são obrigatórios.");
        }

        if (string.IsNullOrWhiteSpace(solucao.DescSolucao))
        {
            return BadRequest("A descrição da solução é obrigatória.");
        }

        _dbContext.Solucao.Add(solucao);
        _dbContext.SaveChanges();

        return CreatedAtAction(nameof(GetSolucao), new { id = solucao.Id }, solucao);
    }

    [HttpPost]
    [Route("associarSolucaoAoTicket/{ticketId}")]
    public async Task<ActionResult> AssociarSolucaoAoTicket(int ticketId, [FromBody] Solucao solucao)
    {
        try
        {
            // Verifique se o Ticket com o ticketId especificado existe
            var ticket = await _dbContext.Ticket.FindAsync(ticketId);

            if (ticket == null)
            {
                return NotFound("Ticket não encontrado.");
            }

            // Associe a Solução ao Ticket
            ticket.Solucao = solucao;

            _dbContext.Ticket.Update(ticket);
            await _dbContext.SaveChangesAsync();

            return Ok("Solução associada ao Ticket com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }


    [HttpPost]
    [Route("associarCategoriaAoTicket/{ticketId}")]
    public async Task<ActionResult> AssociarCategoriaAoTicket(int ticketId, [FromBody] Categoria categoria)
    {
        try
        {
            // Verifique se o Ticket com o ticketId especificado existe
            var ticket = await _dbContext.Ticket.FindAsync(ticketId);

            if (ticket == null)
            {
                return NotFound("Ticket não encontrado.");
            }

            // Associe a Categoria ao Ticket
            ticket.categoria = categoria;

            _dbContext.Ticket.Update(ticket);
            await _dbContext.SaveChangesAsync();

            return Ok("Categoria associada ao Ticket com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }





}
