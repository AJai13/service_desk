using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Models;
using ServiceDesk.Data;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;

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

                await _dbContext.AddAsync(funcionario);
                await _dbContext.SaveChangesAsync();

                return Created("", funcionario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("fazerLogin")]
        public async Task<ActionResult> FazerLogin([FromBody] Funcionario funcionario)
        {
            try
            {
                if (funcionario == null || string.IsNullOrWhiteSpace(funcionario.Email) || string.IsNullOrWhiteSpace(funcionario.Senha))
                {
                    return BadRequest("Invalid login data received.");
                }

                // Find the corresponding Funcionario in the database
                var authenticatedFuncionario = await _dbContext.Funcionario
                    .FirstOrDefaultAsync(f => f.Email == funcionario.Email && f.Senha == funcionario.Senha);

                if (authenticatedFuncionario == null)
                {
                    return Unauthorized("Authentication failed. Email or password is incorrect.");
                }

                // Authentication successful
                return Ok("Authentication successful.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("atualizarPerfil")]
        public async Task<ActionResult> AtualizarPerfil([FromBody] Funcionario updatedFuncionario)
        {
            try
            {
                if (updatedFuncionario == null)
                {
                    return BadRequest("Invalid data received.");
                }

                // Find the corresponding Funcionario in the database
                var existingFuncionario = await _dbContext.Funcionario
                    .FirstOrDefaultAsync(f => f.Email == updatedFuncionario.Email);

                if (existingFuncionario == null)
                {
                    return NotFound("Funcionario not found.");
                }

                // Update properties of the existing Funcionario
                existingFuncionario.Nome = updatedFuncionario.Nome;
                existingFuncionario.Cargo = updatedFuncionario.Cargo;

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                return Ok("Perfil atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }





    }
}
