using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using WebApiCast.Entities;

namespace WebApiCast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SwaggerApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SwaggerApiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("RetornaInformacaoDoCEP01001000")]
        public async Task<ActionResult> GetInformacaoByCEPDefault()
        {
            try
            {
                var urlBase = _configuration.GetSection("MySettings").GetSection("UrlBase").Value;
                var cepBase = "01001000";
            
                var client = new RestClient(urlBase);
                var request = new RestRequest(cepBase + "/json");
                var response = await client.ExecuteGetAsync(request);

                if (response.IsSuccessful)
                {
                    var content = JsonConvert.DeserializeObject<InformacaoLocal>(response.Content);
                    if (content != null)
                        return Ok(content);

                    return BadRequest("Informações do CEP não encontrada!");
                }
                else
                    return BadRequest("CEP inválido!");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // GET api/values
        [HttpGet("RetornaInformacaoDoCEP{cep}")]
        public async Task<ActionResult> GetInformacaoByCEP(string cep)
        {
            try
            {
                var urlBase = _configuration.GetSection("MySettings").GetSection("UrlBase").Value;

                var client = new RestClient(urlBase);
                var request = new RestRequest(cep + "/json");
                var response = await client.ExecuteGetAsync(request);

                if (response.IsSuccessful)
                {
                    var content = JsonConvert.DeserializeObject<InformacaoLocal>(response.Content);
                    if (content != null)
                        return Ok(content);

                    return BadRequest("Informações do CEP não encontrada!");
                }
                else
                    return BadRequest("CEP inválido!");
            }
            catch (Exception ex)
            {

                throw ex;
            }
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
                        context.Add(conta);
                        await context.SaveChangesAsync();
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
                        return NotFound();
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
                            return NotFound();
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