using Microsoft.AspNetCore.Mvc;
using ServiceDesk;
using ServiceDesk.Data;
using Microsoft.EntityFrameworkCore;

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


        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Filtro>>> Listar()
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Filtro is null) return NotFound();
            return await _dbContext.Filtro.ToListAsync();
        }

        [HttpPut]
        [Route("alterar/{id}")]
        public async Task<ActionResult> Alterar(int id, [FromBody] Filtro filtro)
        {
            try
            {
                if (_dbContext == null)
                {
                    return NotFound();
                }

                // Verifique se o filtro com o antigoNome especificado existe
                var filtroExistente = await _dbContext.Filtro.FirstOrDefaultAsync(d => d.Id == id);

                if (filtroExistente == null)
                {
                    return NotFound("Filtro não encontrado.");
                }

                filtroExistente.Nome = filtro.Nome;

                _dbContext.Filtro.Update(filtroExistente);
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
            if (_dbContext.Filtro is null) return NotFound();
            var filtroTemp = await _dbContext.Filtro.FindAsync(id);
            if (filtroTemp is null) return NotFound();
            _dbContext.Remove(filtroTemp);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

    }


}
