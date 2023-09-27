using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Models;
using ServiceDesk.Data;

namespace ServiceDesk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionarioController : ControllerBase
    {
        private readonly ServiceDeskDbContext _dbContext;

        public FuncionarioController(ServiceDeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<ActionResult> Cadastrar([FromBody] Funcionario funcionario)
        {
            try
            {
                if (funcionario == null)
                {
                    return BadRequest("Invalid data received.");
                }

                if (_dbContext is null)
                {
                    return NotFound("Database context not found.");
                }

                if (_dbContext.Funcionario is null)
                {
                    return NotFound("Table 'Funcionario' not found in the database.");
                }

                // Add and save the new record
                await _dbContext.AddAsync(funcionario);
                await _dbContext.SaveChangesAsync();

                return Created("", funcionario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
