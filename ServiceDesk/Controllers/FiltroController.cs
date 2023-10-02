using Microsoft.AspNetCore.Mvc;
using ServiceDesk;
using ServiceDesk.Data;

namespace ServiceDesk.Controllers
{
    public class FiltroController : ControllerBase
    {
        private readonly ServiceDeskDbContext _dbContext;

        public FiltroController(ServiceDeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpPost]
        [Route("setFiltro")]
        public async Task<ActionResult> SetFiltroAsync([FromBody] Filtro filtro)
        {
            if (filtro is null)
            {
                return BadRequest();
            }

            _dbContext.Filtro.Add(filtro);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("GetFiltro", new { id = filtro.Id }, filtro);
        }

 
        [HttpGet("getFiltro/{id}")]
        public async Task<ActionResult<Filtro>> GetFiltro(int id)
        {
            var filtro = await _dbContext.Filtro.FindAsync(id);
            if (filtro == null)
            {
                return NotFound();
            }

            return filtro;
        }




    }


}
