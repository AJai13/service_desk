using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Data;

namespace ServiceDesk;

[ApiController]
[Route("[controller]")]
    public class DispositivoController : ControllerBase
    {
        private DispositivoDbContext? _dbContext;

        public DispositivoController(DispositivoDbContext dbContext) 
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
        public async Task<ActionResult> Alterar(Dispositivo dispositivo){
            if(_dbContext is null) return NotFound();
            if(_dbContext.Dispositivo is null) return NotFound();
            var dispositivoTemp = await _dbContext.Dispositivo.FindAsync(dispositivo.Nome);
            if(dispositivoTemp is null) return NotFound();       
            _dbContext.Dispositivo.Update(dispositivo);
            await _dbContext.SaveChangesAsync();
            return Ok();
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
