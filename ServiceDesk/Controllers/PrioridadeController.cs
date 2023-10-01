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
        var prioridade = _dbContext.Sla.FirstOrDefault(s => s.Id == id);

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


}
