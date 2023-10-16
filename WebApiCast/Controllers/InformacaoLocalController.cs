using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Runtime;
using System.Runtime.Intrinsics.Arm;
using System.Threading;

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

        [HttpGet(Name = "GetInformacaoCEP")]
        public async Task<InformacaoLocal> GetInformacaoByCEP()
        {
            var urlBase = _configuration.GetSection("MySettings").GetSection("UrlBase").Value;
            var cepBase = "01001000";
            
            var client = new RestClient(urlBase);
            var request = new RestRequest(cepBase + "/json");
            var response = await client.GetAsync(request);

            if (response.IsSuccessful)
            {
                var content = JsonConvert.DeserializeObject<InformacaoLocal>(response.Content);
                if (content != null)
                    return content;

                throw new Exception("Informação não encontrada!");
            }
            else
                throw new Exception("CEP não encontrado!");
        }
    }
}