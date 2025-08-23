using CadastroPessoas.Models;

namespace CadastroPessoas.ViewModels
{
    public class PessoaViewModel
    {
        public string Nome { get; set; } = string.Empty;
        public Pessoa.Tipo TipoPessoa { get; set; }
        public string Documento { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Celular { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public string Logradouro { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string? Complemento { get; set; }
        public string Numero { get; set; } = string.Empty;
        public int Codigo { get; set; }
    }
}
