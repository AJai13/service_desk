using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Models;
using ServiceDesk.Data;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Swashbuckle.AspNetCore.Annotations;

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
                    return BadRequest("Dados inválidos ou nulos. Favor validar.");
                }

                if (_dbContext == null)
                {
                    return NotFound("Database não encontrada.");
                }

                if (_dbContext.Funcionario == null)
                {
                    return NotFound("Tabela 'Funcionario' não encontrada.");
                }

                // Defina a propriedade Ticket como nula para remover a serialização
                funcionario.Ticket = null;

                await _dbContext.AddAsync(funcionario);
                await _dbContext.SaveChangesAsync();

                return Created("", funcionario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
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
                    return BadRequest("Login invalido.");
                }


                var authenticatedFuncionario = await _dbContext.Funcionario
                    .FirstOrDefaultAsync(f => f.Email == funcionario.Email && f.Senha == funcionario.Senha);

                if (authenticatedFuncionario == null)
                {
                    return Unauthorized("Autenticação recusada, e-mail ou senha errada.");
                }


                return Ok("Autenticação aceita!.");
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
                    return BadRequest("Dados invalidos, ou nulos. Favor validar.");
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


        [HttpGet]
        [Route("visualizarTickets/{funcionarioId}")]
        public async Task<ActionResult<IEnumerable<Ticket>>> VisualizarTickets(int funcionarioId)
        {
            // Encontre o funcionário pelo ID
            var funcionario = await _dbContext.Funcionario
                .Include(f => f.Ticket) // Inclua os tickets associados ao funcionário
                .FirstOrDefaultAsync(f => f.Id == funcionarioId);

            if (funcionario == null)
            {
                return NotFound();
            }

            var tickets = funcionario.Ticket.ToList();
            return Ok(tickets);
        }

        [HttpGet("listarFuncionarios")]
        public async Task<ActionResult<IEnumerable<Funcionario>>> ListarFuncionarios()
        {
            try
            {
                var funcionarios = await _dbContext.Funcionario.ToListAsync();
                return Ok(funcionarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
            }
        }


    }
}
