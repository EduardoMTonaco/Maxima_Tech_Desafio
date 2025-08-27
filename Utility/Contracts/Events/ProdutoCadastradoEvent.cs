namespace MaximaTech.Contracts.Events
{
    public class ProdutoCadastradoEvent
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}