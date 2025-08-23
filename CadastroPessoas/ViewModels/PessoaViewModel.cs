using CadastroPessoas.Models;

namespace CadastroPessoas.ViewModels
{
    public class PessoaViewModel
    {
        public string Nome { get; set; }
        public Pessoa.Tipo TipoPessoa { get; set; }
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
        public int Codigo { get; set; }
    }
}
