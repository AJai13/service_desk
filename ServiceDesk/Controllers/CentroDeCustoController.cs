using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk;
using ServiceDesk.Data;

[ApiController]
[Route("api/[controller]")]
public class CentroDeCustoController : ControllerBase
{
    private readonly ServiceDeskDbContext _dbContext;

    public CentroDeCustoController(ServiceDeskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult> CadastrarCentroDeCusto([FromBody] CentroDeCusto novoCentroDeCusto)
    {
        if (novoCentroDeCusto == null)
        {
            return BadRequest("Dados inválidos para o novo centro de custo.");
        }

        try
        {
            _dbContext.CentroDeCusto.Add(novoCentroDeCusto);
            await _dbContext.SaveChangesAsync();
            return Created("", novoCentroDeCusto);
        }
        catch (Exception ex)
        {
            // Em caso de erro, retorne uma resposta de erro
            return StatusCode(500, $"Ocorreu um erro ao cadastrar o centro de custo: {ex.Message}");
        }
    }


}
