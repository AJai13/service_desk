using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Data;
using ServiceDesk.Models;

namespace ServiceDesk;
[ApiController]
[Route("[controller]")]
public class TicketController : ControllerBase
{
    private ServiceDeskDbContext? _dbContext;

    public TicketController(ServiceDeskDbContext dbContext)
    {
        _dbContext = dbContext;


    }

    [HttpPost]
    [Route("criarTicket")]
    public async Task<ActionResult> CriarTicket(
        [FromQuery] string titulo,
        [FromQuery] string descricao,
        [FromQuery] string emailUsuarioCriador,
        [FromQuery] string nomeFuncionarioResponsavel,
        [FromQuery] string nomeDispositivo)
    {
        try
        {
            // First, check if the dispositivo with the specified name already exists in the database
            var dispositivoExistente = await _dbContext.Dispositivo.FirstOrDefaultAsync(d => d.Nome == nomeDispositivo);

            if (dispositivoExistente == null)
            {
                // If the dispositivo doesn't exist, you can create a new one and add it to the context
                dispositivoExistente = new Dispositivo
                {
                    Nome = nomeDispositivo
                };
                _dbContext.Dispositivo.Add(dispositivoExistente);
            }

            // Check if the usuario with the specified email already exists in the database
            var usuarioExistente = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.Email == emailUsuarioCriador);

            if (usuarioExistente == null)
            {
                // If the usuario doesn't exist, you can create a new one and add it to the context
                usuarioExistente = new Usuario
                {
                    Email = emailUsuarioCriador
                };
                _dbContext.Usuario.Add(usuarioExistente);
            }

            // Check if the funcionario with the specified name already exists in the database
            var funcionarioExistente = await _dbContext.Funcionario.FirstOrDefaultAsync(f => f.Nome == nomeFuncionarioResponsavel);

            if (funcionarioExistente == null)
            {
                // If the funcionario doesn't exist, you can create a new one and add it to the context
                funcionarioExistente = new Funcionario
                {
                    Nome = nomeFuncionarioResponsavel
                };
                _dbContext.Funcionario.Add(funcionarioExistente);
            }

            // Create the new Ticket with the appropriate relationships
            var novoTicket = new Ticket
            {
                Titulo = titulo,
                Descricao = descricao,
                dataAbertura = DateTime.Now,
                usuarioCriador = usuarioExistente, // Associate the Ticket with Usuario
                funcionarioResponsavel = funcionarioExistente, // Associate the Ticket with Funcionario
                Dispositivo = dispositivoExistente // Associate the Ticket with Dispositivo
            };

            _dbContext.Ticket.Add(novoTicket);
            await _dbContext.SaveChangesAsync();

            return Created("", novoTicket);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }



    [HttpPost]
    [Route("atribuirTicket")]
    public async Task<ActionResult> AtribuirTicket([FromQuery] int ticketId, [FromQuery] int funcionarioId)
    {
        try
        {
            // Certifique-se de que o Ticket com o ID especificado existe
            var ticketExistente = await _dbContext.Ticket.FindAsync(ticketId);

            if (ticketExistente == null)
            {
                return NotFound("Ticket não encontrado.");
            }

            // Certifique-se de que o FuncionarioResponsavel com o ID especificado existe
            var funcionarioExistente = await _dbContext.Funcionario.FindAsync(funcionarioId);

            if (funcionarioExistente == null)
            {
                return NotFound("FuncionarioResponsavel não encontrado.");
            }

            // Atribua o FuncionarioResponsavel ao Ticket
            ticketExistente.funcionarioResponsavel = funcionarioExistente;
            _dbContext.Update(ticketExistente);
            await _dbContext.SaveChangesAsync();

            return Ok("Ticket atribuído ao FuncionarioResponsavel com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }






    [HttpGet]
    [Route("getTicket/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult GetTicket(int id)
    {
        if (_dbContext is null) return NotFound();

        var ticket = _dbContext.Ticket
            .Include(t => t.status)
            .Include(t => t.propriedade)
            .Include(t => t.categoria)
            .Include(t => t.sla)
            .Include(t => t.Dispositivo)
            .Include(t => t.usuarioCriador)
            .Include(t => t.funcionarioResponsavel)
            .FirstOrDefault(t => t.Id == id);

        if (ticket != null)
        {
            return Ok(ticket);
        }

        return NotFound("Ticket não encontrado.");
    }
    [HttpGet]
    [Route("listarTickets")]
    public IActionResult ListarTickets()
    {
        try
        {
            // Use Include to eagerly load related entities (SLA, Categoria, Solucao).
            var tickets = _dbContext.Ticket
                .Include(t => t.sla)
                .Include(t => t.categoria)
                .Include(t => t.Solucao)
                .ToList();

            return Ok(tickets);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpDelete]
    [Route("excluirTicket/{id}")]
    public IActionResult ExcluirTicket(int id)
    {
        try
        {
            // Primeiro, verifique se o ticket com o ID especificado existe no banco de dados.
            var ticketParaExcluir = _dbContext.Ticket.FirstOrDefault(t => t.Id == id);

            if (ticketParaExcluir == null)
            {
                return NotFound($"Ticket com ID {id} não encontrado.");
            }

            // Remova o ticket do contexto e, em seguida, salve as alterações no banco de dados.
            _dbContext.Ticket.Remove(ticketParaExcluir);
            _dbContext.SaveChanges();

            return Ok($"Ticket com ID {id} foi excluído com sucesso.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }



}
