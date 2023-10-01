using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Data;
using System.Threading.Tasks;

namespace ServiceDesk.Controllers
{
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



    }
}
