using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Data;
using System.Threading.Tasks;

namespace ServiceDesk.Controllers
{
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private ServiceDeskDbContext _dbContext;

        public CategoriaController(ServiceDeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("setCategoria")]
        public async Task<ActionResult> SetCategoria([FromBody] Categoria categoria)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Categoria is null) return NotFound();
            await _dbContext.AddAsync(categoria);
            await _dbContext.SaveChangesAsync();

            return Created("", categoria);
        }

        // GET: api/[controller]/getCategoria/{id}
        [HttpGet("getCategoria/{id}")]
        public async Task<ActionResult<Categoria>> GetCategoria(int id)
        {
            var categoria = await _dbContext.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return categoria;
        }

        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Categoria>>> Listar()
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Categoria is null) return NotFound();
            return await _dbContext.Categoria.ToListAsync();
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

        [HttpPut]
        [Route("alterar/{id}")]
        public async Task<ActionResult> Alterar(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (_dbContext == null)
                {
                    return NotFound();
                }

                // Verifique se o categoria com o antigoNome especificado existe
                var categoriaExistente = await _dbContext.Categoria.FirstOrDefaultAsync(d => d.Id == id);

                if (categoriaExistente == null)
                {
                    return NotFound("Categoria não encontrado.");
                }

                categoriaExistente.Nome = categoria.Nome;

                _dbContext.Categoria.Update(categoriaExistente);
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
            if (_dbContext.Categoria is null) return NotFound();
            var categoriaTemp = await _dbContext.Categoria.FindAsync(id);
            if (categoriaTemp is null) return NotFound();
            _dbContext.Remove(categoriaTemp);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
