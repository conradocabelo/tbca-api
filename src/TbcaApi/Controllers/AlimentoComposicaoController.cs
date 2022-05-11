using AngleSharp;
using AngleSharp.Html.Dom;
using Microsoft.AspNetCore.Mvc;
using TbcaApi.Model.ComposicaoAlimentos;
using TbcaApi.Model.Share;

namespace TbcaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlimentoComposicaoController : ControllerBase
    {
        private Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public AlimentoComposicaoController(Microsoft.Extensions.Configuration.IConfiguration configuration) =>
            _configuration = configuration;

        [HttpGet]
        public async Task<List<Alimento>> ConsultarAlimentos([FromQuery] Consulta consulta)
        {
            var config = Configuration.Default.WithDefaultLoader();

            var context = BrowsingContext.New(config);

            var requestUrl = $"{_configuration.GetValue<string>("consulta-composicao")}?pagina={consulta.Pagina}&produto={consulta.Termo}";

            var document = await context.OpenAsync(requestUrl);

            var linhas = document.QuerySelectorAll("tr").Skip(1);

            List<Alimento> lista = new List<Alimento>();

            foreach (var item in linhas)
            {
                var coluns = item.QuerySelectorAll("td");
                
                Alimento alimento = new Alimento();
                
                alimento.Codigo = coluns[0].TextContent;
                alimento.Link = (coluns[0].QuerySelector("a") as IHtmlAnchorElement).Href;
                alimento.Nome = coluns[1].TextContent;
                alimento.NomeIngles = coluns[2].TextContent;
                alimento.NomeCientifico = coluns[3].TextContent;
                alimento.Grupo = coluns[4].TextContent;
                alimento.Marca = coluns[5].TextContent;

                lista.Add(alimento);
            }

            return lista;
        }
    }
}
