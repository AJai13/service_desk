using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ServiceDesk;

[ApiController]
[Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private UsuarioDbContext? _dbContext;

        public UsuarioController(UsuarioDbContext dbContext) 
        { 
            _dbContext = dbContext;
        }

        [HttpPost]
        [Route("cadastrar")]
        public async Task<ActionResult> Cadastrar(Usuario usuario)
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Usuario is null) return NotFound();
            await _dbContext.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
            return Created("",usuario);
        }
        
        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Usuario>>> Listar()
        {
            if (_dbContext is null) return NotFound();
            if (_dbContext.Usuario is null) return NotFound();
            return await _dbContext.Usuario.ToListAsync();
        }

        [HttpGet]
        [Route("buscar/{email}")]
        public async Task<ActionResult<Usuario>> Buscar(string email)
        {
            if(_dbContext is null) return NotFound();
            if(_dbContext.Usuario is null) return NotFound();
            var usuarioTemp = await _dbContext.Usuario.FindAsync(email);
            if(usuarioTemp is null) return NotFound();
            return usuarioTemp;
        }

        [HttpPut]
        [Route("alterar")]
        public async Task<ActionResult> Alterar(Usuario usuario){
            if(_dbContext is null) return NotFound();
            if(_dbContext.Usuario is null) return NotFound();
            var usuarioTemp = await _dbContext.Usuario.FindAsync(usuario.Nome);
            if(usuarioTemp is null) return NotFound();       
            _dbContext.Usuario.Update(usuario);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("excluir/{email}")]
        public async Task<ActionResult> Excluir(string email)
        {
            if(_dbContext is null) return NotFound();
            if(_dbContext.Usuario is null) return NotFound();
            var usuarioTemp = await _dbContext.Usuario.FindAsync(email);
            if(usuarioTemp is null) return NotFound();
            _dbContext.Remove(usuarioTemp);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        }
