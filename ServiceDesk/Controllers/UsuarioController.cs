using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Data;

namespace ServiceDesk;

[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private ServiceDeskDbContext? _dbContext;

    public UsuarioController(ServiceDeskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost]
    [Route("cadastrar")]
    public async Task<ActionResult> Cadastrar([FromBody] Usuario usuario)
    {
        if (_dbContext == null)
        {
            return NotFound();
        }
        
        await _dbContext.AddAsync(usuario);
        await _dbContext.SaveChangesAsync();
        return Created("", usuario);
    }



    [HttpGet]
    [Route("buscar/{email}")]
    public async Task<ActionResult> Buscar(string email)
    {
        if (_dbContext == null)
        {
            return NotFound();
        }

        var usuario = await _dbContext.Usuario.Include(u => u.Dispositivo).ToListAsync();


        if (usuario == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        return Ok(usuario);
    }

    [HttpGet]
    [Route("listarUsuarios")]
    public async Task<ActionResult<IEnumerable<Usuario>>> ListarUsuarios()
    {
        try
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            var usuarios = await _dbContext.Usuario
                .Include(u => u.Dispositivo) // Include the usuario navigation property
                .ToListAsync();

            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }

    [HttpPut]
    [Route("alterar/{id}")]
    public async Task<ActionResult> Alterar(int id, [FromBody] Usuario usuario)
    {
        try
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            // Verifique se o usuario com o antigoNome especificado existe
            var usuarioExistente = await _dbContext.Usuario.FirstOrDefaultAsync(d => d.Id == id);

            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.CentroDeCustoId = usuario.CentroDeCustoId;
            usuarioExistente.DispositivoId = usuario.DispositivoId;

            _dbContext.Usuario.Update(usuarioExistente);
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
        if (_dbContext.Usuario is null) return NotFound();
        var usuarioTmp = await _dbContext.Usuario.FindAsync(id);
        if (usuarioTmp is null) return NotFound();
        _dbContext.Remove(usuarioTmp);
        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}
