using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using WebApiCast.Entities;

namespace WebApiCast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IContaRepository _contaRepository;

        public ContaController(IConfiguration configuration, IContaRepository contaRepository)
        {
            _configuration = configuration;
            _contaRepository = contaRepository; 
        }

        // GET: Contas/Create
        [HttpPost("InserirConta")]
        public async Task<IActionResult> Inserir([Bind("Nome,Descricao")] Conta conta)
        {
            try
            {
                using (var context = new DataContext())
                {
                    if (ModelState.IsValid)
                    {
                        await _contaRepository.Adicionar(conta);
                        return Ok("A conta foi cadastrada com sucesso!");
                    }
                    return BadRequest("Erro!");

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        // GET: Contas/Edit/5
        [HttpPut("EditarConta")]
        public async Task<IActionResult> Editar([FromBody] Conta conta)
        {

            try
            {
                using (var context = new DataContext())
                {
                    if (context.Contas == null)
                    {
                        return Problem("Não há registros de contas no Banco de dados!");
                    }

                    try
                    {
                        context.Update(conta);
                        await context.SaveChangesAsync();
                        return Ok("A conta foi atualizado com sucesso!");
                    }
                    catch (DbUpdateConcurrencyException ex)
                    {
                        if (!ContaExiste(conta.Id))
                        {
                            return Problem("Não há dados referente a este ID!");
                        }
                        else
                        {
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // POST: Contas/Delete/5
        [HttpPost("DeletarContaPorId")]
        public async Task<IActionResult> Deletar(int id)
        {
            using(var context = new DataContext()) 
            {
                if (context.Contas == null)
                {
                    return Problem("Não há registros de contas no Banco de dados!");
                }
                var conta = await context.Contas.FindAsync(id);
                if (conta != null)
                {
                    context.Contas.Remove(conta);
                }

                await context.SaveChangesAsync();
                return Ok("A conta foi deletada com sucesso!");

            }

        }

        [HttpGet("RetornarContaPorId")]
        public async Task<IActionResult> RetornarConta(int id)
        {
            using (var context = new DataContext())
            {
                if (context.Contas == null)
                {
                    return Problem("Não há registros de contas no Banco de dados!");
                }
                var conta = await context.Contas.FindAsync(id);
                if (conta != null)
                {
                    return Ok(conta);
                }

                return Problem("Não há dados referente a este ID!");
            }
        }

        // GET: Contas
        [HttpGet("ListarTodasContas")]
        public async Task<IActionResult> ListarTodos()
        {
            using (var context = new DataContext())
            {
                return context.Contas != null ?
                          Ok(await context.Contas.ToListAsync()) : Problem("Não há registros de contas no Banco de dados!");
            }
        }

        private bool ContaExiste(int id)
        {
            using(var context = new DataContext())
            {
                return (context.Contas?.Any(e => e.Id == id)).GetValueOrDefault();
            }
        }

    }
}