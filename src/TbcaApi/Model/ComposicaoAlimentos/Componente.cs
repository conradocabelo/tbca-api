namespace TbcaApi.Model.ComposicaoAlimentos
{
    public class Componente
    {
        public string Nome { get; set; }
        public string Unidade { get; set; }
        public List<Valor> Valores { get; set; }
    }
}
