using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.Intrinsics.Arm;
using System.Threading;
using WebApiCast.Entities;

namespace WebApiCast.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InformacaoLocalController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public InformacaoLocalController(IConfiguration configuration)
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
    }
}