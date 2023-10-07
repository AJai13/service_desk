using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Data;

namespace ServiceDesk;

[ApiController]
[Route("[controller]")]
    public class DispositivoController : ControllerBase
    {
        private ServiceDeskDbContext? _dbContext;

        public DispositivoController(ServiceDeskDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<ActionResult> Cadastrar(Dispositivo dispositivo)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Dispositivo is null) return NotFound();
            await _dbContext.AddAsync(dispositivo);
            await _dbContext.SaveChangesAsync();
            return Created("",dispositivo);
        }
        
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Dispositivo>>> Listar()
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Dispositivo is null) return NotFound();
            return await _dbContext.Dispositivo.ToListAsync();
        }

        [HttpGet]
        [Route("buscar/{id}")]
        public async Task<ActionResult<Dispositivo>> Buscar(int id)
        {
            if(_dbContext is null) return NotFound();
            if(_dbContext.Dispositivo is null) return NotFound();
            var dispositivoTemp = await _dbContext.Dispositivo.FindAsync(id);
            if(dispositivoTemp is null) return NotFound();
            return dispositivoTemp;
        }

    [HttpPut]
    [Route("alterar")]
    public async Task<ActionResult> Alterar([FromQuery] string antigoNome, [FromQuery] string novoNome)
    {
        try
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            // Verifique se o dispositivo com o antigoNome especificado existe
            var dispositivoExistente = await _dbContext.Dispositivo.FirstOrDefaultAsync(d => d.Nome == antigoNome);

            if (dispositivoExistente == null)
            {
                return NotFound("Dispositivo não encontrado.");
            }

            // Realize as atualizações necessárias no dispositivoExistente
            dispositivoExistente.Nome = novoNome;
            // ...

            _dbContext.Dispositivo.Update(dispositivoExistente);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno do servidor: {ex.Message}");
        }
    }


    [HttpPost]
    [Route("associarUsuarioAoDispositivo/{dispositivoId}")]
    public async Task<ActionResult> AssociarUsuarioAoDispositivo(int dispositivoId, [FromBody] int usuarioId)
    {
        try
        {
            // Verifique se o Dispositivo com o dispositivoId especificado existe
            var dispositivo = await _dbContext.Dispositivo.FindAsync(dispositivoId);

            if (dispositivo == null)
            {
                return NotFound("Dispositivo não encontrado.");
            }

            // Verifique se o Usuario com o usuarioId especificado existe
            var usuario = await _dbContext.Usuario.FindAsync(usuarioId);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            // Associe o Usuário ao Dispositivo
            //dispositivo.Usuario = usuario;
            _dbContext.Dispositivo.Update(dispositivo);
            await _dbContext.SaveChangesAsync();

            return Ok("Usuário associado ao Dispositivo com sucesso.");
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
            if(_dbContext is null) return NotFound();
            if(_dbContext.Dispositivo is null) return NotFound();
            var dispositivoTemp = await _dbContext.Dispositivo.FindAsync(id);
            if(dispositivoTemp is null) return NotFound();
            _dbContext.Remove(dispositivoTemp);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }


        }
