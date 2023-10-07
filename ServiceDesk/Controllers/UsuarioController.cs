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
    public async Task<ActionResult> Cadastrar(
    [FromQuery] string nome,
    [FromQuery] string email,
    [FromQuery] string? dispositivo,
    [FromQuery] string? centroDeCusto)
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            var usuario = new Usuario
            {
                Nome = nome,
                Email = email
            };

            if (!string.IsNullOrEmpty(dispositivo))
            {
                usuario.Dispositivo = new Dispositivo { Nome = dispositivo };
            }

            if (!string.IsNullOrEmpty(centroDeCusto))
            {
                usuario.CentroDeCusto = new CentroDeCusto { Nome = centroDeCusto };
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

    [HttpPut]
    [Route("alterar")]
    public async Task<ActionResult> Alterar([FromBody] Usuario usuario, string email)
    {
        try
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            var usuarioExistente = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.Email == email);

            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.Dispositivo = usuario.Dispositivo;
            usuarioExistente.CentroDeCusto = usuario.CentroDeCusto;

            _dbContext.Usuario.Update(usuarioExistente);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
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
                .Include(u => u.Dispositivo) // Include the Dispositivo navigation property
                .ToListAsync();

            return Ok(usuarios);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }


    [HttpDelete]
    [Route("excluir/{email}")]
    public async Task<ActionResult> Excluir(string email)
    {
        try
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            var usuarioExistente = await _dbContext.Usuario.FirstOrDefaultAsync(u => u.Email == email);

            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _dbContext.Usuario.Remove(usuarioExistente);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }


}
