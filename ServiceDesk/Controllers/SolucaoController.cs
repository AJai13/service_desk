using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

}
