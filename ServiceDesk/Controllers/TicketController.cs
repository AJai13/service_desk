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
    public async Task<ActionResult> CriarTicket([FromBody] Ticket ticket)
    {
        //Iniciar tudo em Try 
        try
        {
            _dbContext.Ticket.Add(ticket);
            await _dbContext.SaveChangesAsync();

            return Created("Ticket criado", ticket);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }



    [HttpPost]
    [Route("atribuirTicket")]
    public async Task<ActionResult> AtribuirTicket([FromQuery] int ticketId, [FromQuery] int funcionarioId)
    {
        try
        {

            var ticketExistente = await _dbContext.Ticket.FindAsync(ticketId);

            if (ticketExistente == null)
            {
                return NotFound("Ticket não encontrado.");
            }


            var funcionarioExistente = await _dbContext.Funcionario.FindAsync(funcionarioId);

            if (funcionarioExistente == null)
            {
                return NotFound("FuncionarioResponsavel não encontrado.");
            }

            //Atribui um funcionario como responsavel do ticket
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
    public ActionResult GetTicket(int id)
    {
        if (_dbContext is null) return NotFound();

        var ticket = _dbContext.Ticket
            .Include(t => t.status)
            .Include(t => t.prioridade)
            .Include(t => t.categoria)
            .Include(t => t.sla)
            .Include(t => t.Dispositivo)
            .Include(t => t.usuarioCriador)
            .Include(t => t.Solucao)
            .Include(t => t.funcionarioResponsavel)
            .FirstOrDefault(t => t.Id == id);
        //retorna primeiro elemento de uma sequencia    

        if (ticket != null)
        {
            return Ok(ticket);
        }

        return NotFound("Ticket não encontrado.");
    }

    //IactionResult = retornar varios tipos HTTTP
    [HttpGet]
    [Route("listarTickets")]
    public IActionResult ListarTickets()
    {
        try
        {
            //Include para carregar todos os relacionamentos, .ToList converter
            var tickets = _dbContext.Ticket
            .Include(t => t.status)
            .Include(t => t.prioridade)
            .Include(t => t.categoria)
            .Include(t => t.sla)
            .Include(t => t.Dispositivo)
            .Include(t => t.usuarioCriador)
            .Include(t => t.Solucao)
            .Include(t => t.funcionarioResponsavel)
                .ToList();

            return Ok(tickets);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpPut]
    [Route("alterar/{id}")]
    public async Task<ActionResult> Alterar(int id, [FromBody] Ticket ticket)
    {
        try
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            // Verifique se o ticket com o antigoNome especificado existe
            var ticketExistente = await _dbContext.Ticket.FirstOrDefaultAsync(d => d.Id == id);

            if (ticketExistente == null)
            {
                return NotFound("Ticket não encontrado.");
            }

            ticketExistente.Titulo = ticket.Titulo;
            ticketExistente.Descricao = ticket.Descricao;
            ticketExistente.dataAbertura = ticket.dataAbertura;
            ticketExistente.StatusId = ticket.StatusId;
            ticketExistente.PrioridadeId = ticket.PrioridadeId;
            ticketExistente.CategoriaId = ticket.CategoriaId;
            ticketExistente.DispositivoId = ticket.DispositivoId;
            ticketExistente.UsuarioId = ticket.UsuarioId;
            ticketExistente.FuncionarioId = ticket.FuncionarioId;
            ticketExistente.SolucaoId = ticket.SolucaoId;
            ticketExistente.SlaId = ticket.SlaId;

            _dbContext.Ticket.Update(ticketExistente);
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
        if (_dbContext.Ticket is null) return NotFound();
        var ticketTmp = await _dbContext.Ticket.FindAsync(id);
        if (ticketTmp is null) return NotFound();
        _dbContext.Remove(ticketTmp);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
