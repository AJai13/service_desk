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
     [FromQuery] string nomeDispositivo,
     [FromQuery] string nomeFuncionarioResponsavel)
    {
        try
        {
            // Aqui, você pode criar um novo objeto Ticket com os campos recebidos como parâmetros.
            var novoTicket = new Ticket
            {
                Titulo = titulo,
                Descricao = descricao,
                dataAbertura = DateTime.Now, // Data atual como padrão
                usuarioCriador = new Usuario
                {
                    Email = emailUsuarioCriador
                },
                funcionarioResponsavel = new Funcionario
                {
                    Nome = nomeFuncionarioResponsavel
                }
            };

            // Adicione a lógica para salvar o novoTicket no banco de dados.
            // Certifique-se de configurar o funcionário responsável corretamente, se necessário.

            // Retorna uma resposta de sucesso.
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
            var tickets = _dbContext.Ticket.ToList();
            return Ok(tickets);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

}
