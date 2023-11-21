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

    [HttpGet]
    [Route("listar")]
    public async Task<ActionResult<IEnumerable<Solucao>>> Listar()
    {
        if (_dbContext is null) return NotFound();
        if (_dbContext.Solucao is null) return NotFound();
        return await _dbContext.Solucao.ToListAsync();
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

    [HttpPut]
    [Route("associarSolucaoAoTicket/{ticketId}")]
    public async Task<ActionResult> AssociarSolucaoAoTicket(int ticketId, [FromBody] int solucaoId)
    {
        try
        {
            // Verifique se o Ticket com o ticketId especificado existe
            var ticket = await _dbContext.Ticket.FindAsync(ticketId);

            if (ticket == null)
            {
                return NotFound("Ticket não encontrado.");
            }

            // Verifique se a Solucao com o solucaoId especificado existe
            var solucao = await _dbContext.Solucao.FindAsync(solucaoId);

            if (solucao == null)
            {
                return NotFound("Solucao não encontrada.");
            }

            // Associe a Solucao ao Ticket
            ticket.Solucao = solucao;

            _dbContext.Ticket.Update(ticket);
            await _dbContext.SaveChangesAsync();

            return Ok("Solucao associada ao Ticket com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpPut]
    [Route("alterar/{id}")]
    public async Task<ActionResult> Alterar(int id, [FromBody] Solucao solucao)
    {
        try
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            // Verifique se o solucao com o antigoNome especificado existe
            var solucaoExistente = await _dbContext.Solucao.FirstOrDefaultAsync(d => d.Id == id);

            if (solucaoExistente == null)
            {
                return NotFound("Solucão não encontrado.");
            }

            solucaoExistente.DescSolucao = solucao.DescSolucao;

            _dbContext.Solucao.Update(solucaoExistente);
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
        if (_dbContext.Solucao is null) return NotFound();
        var solucaoTmp = await _dbContext.Solucao.FindAsync(id);
        if (solucaoTmp is null) return NotFound();
        _dbContext.Remove(solucaoTmp);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }



}
