using APIPerson.Data.DTO;
using APIPerson.Repository;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Threading.Tasks;

namespace APIPerson.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly IPersonRepository _pessoaRepository;

        private readonly IDapperService _dapperService;
        public PersonController(IPersonRepository pessoaRepository, IDapperService dapperService)
        {
            _pessoaRepository = pessoaRepository;
            _dapperService = dapperService;
        }

        [Route("/GetAllPerson")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable>> GetAllPerson()
        {
            try
            {
                List<PersonDTO> person = await _pessoaRepository.FindAllPerson();

                if (person.Count == 0)
                    return BadRequest(new
                    {
                        Message = "Nenhuma pessoa registrada."
                    });

                return Ok(new
                {
                    Person = person
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

        [Route("/GetPersonById")]
        [HttpGet]
        public async Task<ActionResult> GetPersonById(long id)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var person = await _pessoaRepository.FindPersonById(id);

                return Ok(new
                {
                    Person = person
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

        [Route("/CreatePerson")]
        [HttpPost]
        public async Task<ActionResult> CreatePerson(PersonDTO personDTO)
        {
            string json;

            try
            {
                if (personDTO == null)
                    BadRequest("Pessoa não encontrada");

                var parms = new DynamicParameters();
                parms.Add("PersonId", personDTO.Id);
                parms.Add("PersonName", personDTO.Name, DbType.String);
                parms.Add("PersonAge", personDTO.Age, DbType.Int64);
                parms.Add("PersonGender", personDTO.Gender, DbType.String);
                parms.Add("PersonBirthDate", personDTO.BirthDate, DbType.DateTime);

                var person = Task.FromResult(_dapperService.Insert<ActionResult>("[dbo].[InsertPerson]", parms, commandType: CommandType.StoredProcedure));

                json = JsonSerializer.Serialize(person);

                return Ok(new
                {
                    Message = "Pessoa criada com sucesso",
                    Person = json
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

        [Route("/UpdatePerson")]
        [HttpPut]
        public async Task<ActionResult> UpdatePerson(PersonDTO personDTO)
        {
            string json;

            try
            {
                if (personDTO == null)
                    return BadRequest("Pessoa não encontrada");

                var parms = new DynamicParameters();
                parms.Add("PersonId", personDTO.Id);
                parms.Add("PersonName", personDTO.Name, DbType.String);
                parms.Add("PersonAge", personDTO.Age, DbType.Int64);
                parms.Add("PersonGender", personDTO.Gender, DbType.String);
                parms.Add("PersonBirthDate", personDTO.BirthDate, DbType.DateTime);

                var person = Task.FromResult(_dapperService.Update<ActionResult>("[dbo].[UpdatePerson]", parms, commandType: CommandType.StoredProcedure));

                json = JsonSerializer.Serialize(person);

                return Ok(new
                {
                    Person = json
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

        [Route("/DeletePerson")]
        [HttpDelete()]
        public async Task<ActionResult> DeletePerson(long id)
        {
            string json;

            try
            {
                var person = _pessoaRepository.DeletePerson(id);

                json = JsonSerializer.Serialize(person);

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

        [Route("/DeleteAllPerson")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAllPerson()
        {
            string json;

            try
            {
                bool personDelete = await _pessoaRepository.DeleteAll();

                json = JsonSerializer.Serialize(personDelete);

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

