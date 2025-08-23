using CadastroPessoas.Models;
using System.Collections;

namespace CadastroPessoas.Interfaces
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<Pessoa>> GetPessoas();
        Task<IEnumerable<Pessoa>> GetPessoasByNome(string nome);
        Task<Pessoa?> GetPessoasByCodigo(int codigo);
        Task CreatePessoa(Pessoa pessoa);
        Task UpdatePessoa(Pessoa pessoa);
        Task DeletePessoa(Pessoa pessoa);
    }
}
