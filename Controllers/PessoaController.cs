using ListDePessoas.Data.DTO;
using ListDePessoas.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace ListDePessoas.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PessoaController : ControllerBase
    {

        private readonly IPessoaRepository _pessoaRepository;

        public PessoaController(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        [Route("/FindAllPessoas")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetAllPessoas()
        {
            try
            {
                List<PessoaDTO> pessoa = await _pessoaRepository.FindAllPessoa();

                if (pessoa.Count == 0)
                    return Ok(new
                    {
                        Message = "Nenhuma pessoa registrada."
                    });

                return Ok(new
                {
                    Pessoa = pessoa
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = "false",
                    Message = ex.Message
                });
            }
        }

        [Route("/GetPessoaById")]
        [HttpGet]
        public async Task<ActionResult> GetById(long id)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var pessoa = await _pessoaRepository.FindPessoaById(id);

                return Ok(new
                {
                    Pessoa = pessoa
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = "false",
                    Message = ex.Message
                });
            }
        }

        [Route("/CreatePessoa")]
        [HttpPost]
        public async Task<ActionResult> CreatePessoa(PessoaDTO person)
        {
            string json;

            try
            {
                if (person == null)
                    BadRequest("Pessoa não encontrada");

                PessoaDTO pessoa = await _pessoaRepository.CreatePessoa(person);

                json = JsonSerializer.Serialize(pessoa);

                return Ok(new
                {
                    Message = "Pessoa criada com sucesso",
                    Pessoa = json
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = "false",
                    Message = ex.Message
                });
            }
        }

        [Route("/UpdatePessoa")]
        [HttpPut]
        public async Task<ActionResult> UpdatePessoa(PessoaDTO person)
        {
            string json;

            try
            {
                if (person == null)
                    return BadRequest("Pessoa não encontrada");

                var pessoa = await _pessoaRepository.UpdatePessoa(person);

                json = JsonSerializer.Serialize(pessoa);

                return Ok(new
                {
                    Pessoa = json
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = "false",
                    Message = ex.Message
                });
            }
        }

        [Route("/DeletePessoaById")]
        [HttpDelete()]
        public async Task<ActionResult> DeletePessoa(long id)
        {
            string json;

            try
            {
                var pessoa = _pessoaRepository.DeletePessoa(id);

                json = JsonSerializer.Serialize(pessoa);

                return Ok(new
                {
                    Success = "true",
                    Message = "Pessoa deletada com sucesso."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = "false",
                    Message = ex.Message
                });
            }
        }

        [Route("/DeleteAll")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAllPessoa()
        {
            string json;

            try
            {
                bool pessoaDeletada = await _pessoaRepository.DeleteAll();

                json = JsonSerializer.Serialize(pessoaDeletada);

                return Ok(new
                {
                    Success = json,
                    Message = "Método executado com sucesso."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Success = "false",
                    Message = ex.Message
                });
            }
        }
    }
}

