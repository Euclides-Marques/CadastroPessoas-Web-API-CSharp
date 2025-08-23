using CadastroPessoas.Context;
using CadastroPessoas.Interfaces;
using CadastroPessoas.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroPessoas.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly AppDbContext _dbContext;

        public PessoaRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Pessoa>> GetPessoas()
        {
            return await _dbContext.Pessoas.Where(p => p.Ativo).ToListAsync();
        }

        public async Task<IEnumerable<Pessoa>> GetPessoasByNome(string nome)
        {
            if (!string.IsNullOrEmpty(nome))
            {
                return await _dbContext.Pessoas.Where(p => p.Nome.Contains(nome) && p.Ativo).ToListAsync();
            }

            return await GetPessoas();
        }

        public async Task<Pessoa?> GetPessoasByCodigo(int codigo)
        {
            return await _dbContext.Pessoas.FirstOrDefaultAsync(p => p.Codigo == codigo && p.Ativo);
        }

        public async Task CreatePessoa(Pessoa pessoa)
        {
            pessoa.Id = Guid.NewGuid();
            pessoa.Ativo = true;
            pessoa.DataInclusao = DateTime.Now;
            pessoa.DataAlteracao = DateTime.Now;

            _dbContext.Pessoas.Add(pessoa);
            
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePessoa(Pessoa pessoa)
        {
            var pessoaExiste = await _dbContext.Pessoas.FindAsync(pessoa.Id);

            if (pessoaExiste == null)
            {
                await CreatePessoa(pessoa);
            }
            else
            {
                pessoaExiste.DataAlteracao = DateTime.Now;
                pessoaExiste.DataInclusao = pessoa.DataInclusao;
                pessoaExiste.Codigo = pessoa.Codigo;

                _dbContext.Pessoas.Entry(pessoa).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeletePessoa(Pessoa pessoa)
        {
            var pessoaExiste = await _dbContext.Pessoas.FindAsync(pessoa.Id);

            if (pessoaExiste == null)
            {
                await CreatePessoa(pessoa);
            }
            else
            {
                pessoaExiste.Ativo = false;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
