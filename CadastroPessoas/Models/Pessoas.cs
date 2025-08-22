namespace CadastroPessoas.Models
{
    public class Pessoa
    {
        public Guid Id { get; set; }
        public DateTime DataInclusao { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public Tipo TipoPessoa { get; set; }
        public string Documento { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Bairro { get; set; }
        public string? Complemento { get; set; }
        public string Numero { get; set; }
        public DateTime DataAlteracao { get; set; }

        public enum Tipo
        {
            Fisica = 0,
            Juridica = 1
        }
    }
}
