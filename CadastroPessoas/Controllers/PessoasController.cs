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
            } catch
            {
                return BadRequest("Erro ao obter os dados!");
            }
        }

        [HttpGet]
        [Route("/PessoasByNome")]
        public async Task<ActionResult<IAsyncEnumerable<Pessoa>>> GetPessoasByNome([FromQuery] string nome)
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
        [Route("/PessoasBy/{codigo}")]
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
        public async Task<ActionResult<Pessoa>> CreatePessoa(Pessoa pessoa)
        {
            try
            {
                await _pessoaRepository.CreatePessoa(pessoa);

                return CreatedAtRoute(nameof(GetPessoasByCodigo), new { codigo = pessoa.Codigo }, pessoa);
            }
            catch
            {
                return BadRequest("Request inválido!");
            }
        }

        [HttpPut]
        [Route("/UpdatePessoa/{codigo}")]
        public async Task<ActionResult> UpdatePessoa(int codigo, [FromBody] Pessoa pessoa)
        {
            try
            {
                if(pessoa.Codigo == codigo)
                {
                    await _pessoaRepository.UpdatePessoa(pessoa);
                    return Ok("Pessoa atualizada com sucesso");
                }

                return BadRequest("Erro ao atualizar pessoa!");
            }
            catch
            {
                return BadRequest("Request inválido!");
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
