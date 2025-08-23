using CadastroPessoas.Interfaces;
using CadastroPessoas.Models;
using CadastroPessoas.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CadastroPessoas.Controllers
{
    [ApiController]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoasController(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        [HttpGet]
        [Route("/Pessoas")]
        public async Task<ActionResult<IEnumerable<PessoaViewModel>>> Get()
        {
            try
            {
                var pessoas = await _pessoaRepository.GetPessoas();

                var pessoasViewModel = pessoas.Select(p => new PessoaViewModel
                {
                    Nome = p.Nome,
                    TipoPessoa = p.TipoPessoa,
                    Documento = p.Documento,
                    DataNascimento = p.DataNascimento,
                    Celular = p.Celular,
                    Email = p.Email,
                    Cep = p.Cep,
                    Logradouro = p.Logradouro,
                    Cidade = p.Cidade,
                    Estado = p.Estado,
                    Bairro = p.Bairro,
                    Complemento = p.Complemento,
                    Numero = p.Numero,
                    Codigo = p.Codigo
                });

                return Ok(pessoasViewModel);
            }
            catch
            {
                return BadRequest("Erro ao obter os dados!");
            }
        }

        [HttpGet]
        [Route("/PessoasByNome")]
        public async Task<ActionResult<IAsyncEnumerable<PessoaViewModel>>> GetPessoasByNome([FromQuery] string nome)
        {
            try
            {
                var pessoas = await _pessoaRepository.GetPessoasByNome(nome);

                var pessoasViewModel = pessoas.Select(p => new PessoaViewModel
                {
                    Nome = p.Nome,
                    TipoPessoa = p.TipoPessoa,
                    Documento = p.Documento,
                    DataNascimento = p.DataNascimento,
                    Celular = p.Celular,
                    Email = p.Email,
                    Cep = p.Cep,
                    Logradouro = p.Logradouro,
                    Cidade = p.Cidade,
                    Estado = p.Estado,
                    Bairro = p.Bairro,
                    Complemento = p.Complemento,
                    Numero = p.Numero,
                    Codigo = p.Codigo
                });

                return Ok(pessoasViewModel);
            }
            catch
            {
                return BadRequest("Request inválido!");
            }
        }

        [HttpGet]
        [Route("/PessoasBy/{codigo}", Name = "GetPessoasByCodigo")]
        public async Task<ActionResult<PessoaViewModel>> GetPessoasByCodigo(int codigo)
        {
            try
            {
                var pessoa = await _pessoaRepository.GetPessoasByCodigo(codigo);

                if (pessoa == null)
                {
                    return NotFound($"Não existe pessoa com o código = {codigo}");
                }

                var pessoaViewModel = new PessoaViewModel
                {
                    Nome = pessoa.Nome,
                    TipoPessoa = pessoa.TipoPessoa,
                    Documento = pessoa.Documento,
                    DataNascimento = pessoa.DataNascimento,
                    Celular = pessoa.Celular,
                    Email = pessoa.Email,
                    Cep = pessoa.Cep,
                    Logradouro = pessoa.Logradouro,
                    Cidade = pessoa.Cidade,
                    Estado = pessoa.Estado,
                    Bairro = pessoa.Bairro,
                    Complemento = pessoa.Complemento,
                    Numero = pessoa.Numero,
                    Codigo = pessoa.Codigo
                };

                return Ok(pessoaViewModel);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter a pessoa");
            }
        }

        [HttpPost]
        [Route("/CreatePessoa")]
        public async Task<ActionResult<PessoaViewModel>> CreatePessoa([FromBody] PessoaViewModel pessoaViewModel)
        {
            try
            {
                var pessoa = new Pessoa
                {
                    Nome = pessoaViewModel.Nome,
                    TipoPessoa = pessoaViewModel.TipoPessoa,
                    Documento = pessoaViewModel.Documento,
                    DataNascimento = pessoaViewModel.DataNascimento.Date,
                    Celular = pessoaViewModel.Celular,
                    Email = pessoaViewModel.Email,
                    Cep = pessoaViewModel.Cep,
                    Logradouro = pessoaViewModel.Logradouro,
                    Cidade = pessoaViewModel.Cidade,
                    Estado = pessoaViewModel.Estado,
                    Bairro = pessoaViewModel.Bairro,
                    Complemento = pessoaViewModel.Complemento,
                    Numero = pessoaViewModel.Numero,
                    Codigo = pessoaViewModel.Codigo,
                    Id = Guid.NewGuid(),
                    Ativo = true,
                    DataInclusao = DateTime.Now,
                    DataAlteracao = DateTime.Now
                };

                await _pessoaRepository.CreatePessoa(pessoa);

                pessoaViewModel.Codigo = pessoa.Codigo;

                var routeValues = new { codigo = pessoa.Codigo };
                var routeName = "GetPessoasByCodigo";
                return CreatedAtRoute(routeName, routeValues: routeValues, value: pessoaViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar pessoa: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("/UpdatePessoa/{codigo}")]
        public async Task<ActionResult> UpdatePessoa(int codigo, [FromBody] PessoaViewModel pessoaViewModel)
        {
            try
            {
                var existingPessoa = await _pessoaRepository.GetPessoasByCodigo(codigo);
                if (existingPessoa == null)
                {
                    return NotFound($"Pessoa com código {codigo} não encontrada");
                }

                existingPessoa.Nome = pessoaViewModel.Nome;
                existingPessoa.TipoPessoa = pessoaViewModel.TipoPessoa;
                existingPessoa.Documento = pessoaViewModel.Documento;
                existingPessoa.DataNascimento = pessoaViewModel.DataNascimento;
                existingPessoa.Celular = pessoaViewModel.Celular;
                existingPessoa.Email = pessoaViewModel.Email;
                existingPessoa.Cep = pessoaViewModel.Cep;
                existingPessoa.Logradouro = pessoaViewModel.Logradouro;
                existingPessoa.Cidade = pessoaViewModel.Cidade;
                existingPessoa.Estado = pessoaViewModel.Estado;
                existingPessoa.Bairro = pessoaViewModel.Bairro;
                existingPessoa.Complemento = pessoaViewModel.Complemento;
                existingPessoa.Numero = pessoaViewModel.Numero;

                existingPessoa.DataAlteracao = DateTime.Now;

                await _pessoaRepository.UpdatePessoa(existingPessoa);
                return Ok("Pessoa atualizada com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar pessoa: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("/DeletePessoa/{codigo}")]
        public async Task<ActionResult> DeletePessoa(int codigo)
        {
            try
            {
                var pessoa = await _pessoaRepository.GetPessoasByCodigo(codigo);

                if (pessoa != null)
                {
                    await _pessoaRepository.DeletePessoa(pessoa);
                    return Ok("Pessoa deletada com sucesso!");
                }

                return BadRequest("Erro ao deletar pessoa!");
            }
            catch
            {
                return BadRequest("Request inválido!");
            }
        }
    }
}
