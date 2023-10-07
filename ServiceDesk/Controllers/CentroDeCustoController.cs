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
    [Route("associarCategoriaAoTicket/{ticketId}/{categoryId}")]
    public async Task<ActionResult> AssociarCategoriaAoTicket(int ticketId, int categoryId)
    {
        try
        {
            // Encontre o ticket pelo ID
            var ticket = await _dbContext.Ticket.FindAsync(ticketId);

            if (ticket == null)
            {
                return NotFound("Ticket não encontrado.");
            }

            // Encontre a categoria pelo ID
            var categoria = await _dbContext.Categoria.FindAsync(categoryId);

            if (categoria == null)
            {
                return NotFound("Categoria não encontrada.");
            }

            // Associe a categoria ao ticket
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




    [HttpPost]
    [Route("associarUsuarioAoCentroDeCusto/{usuarioId}")]
    public IActionResult AssociarUsuarioAoCentroDeCusto(int usuarioId, [FromBody] int centroDeCustoId)
    {
        try
        {
            // Encontre o usuário pelo ID
            var usuario = _dbContext.Usuario.FirstOrDefault(u => u.Id == usuarioId);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Encontre o centro de custo pelo ID
            var centroDeCusto = _dbContext.CentroDeCusto.FirstOrDefault(c => c.Id == centroDeCustoId);
            if (centroDeCusto == null)
            {
                return NotFound("Centro de custo não encontrado.");
            }

            // Associe o usuário ao centro de custo
            usuario.CentroDeCusto = centroDeCusto;
            _dbContext.SaveChanges();

            return Ok("Usuário associado ao centro de custo com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }


    [HttpGet]
    [Route("listarCentrosDeCusto")]
    public IActionResult ListarCentrosDeCusto()
    {
        try
        {
            var centrosDeCustoComUsuarios = _dbContext.CentroDeCusto.Include(c => c.Usuario).ToList();
            return Ok(centrosDeCustoComUsuarios);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }



}
