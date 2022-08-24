using APIPerson.Data.DTO;
using APIPerson.Model;
using APIPerson.Model.Context;
using APIPerson.Transaction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIPerson.Repository
{
    public class PersonReporitory : IPersonRepository
    {
        private readonly ContextPerson _context;

        public PersonReporitory(ContextPerson context)
        {
            _context = context;
        }

        public async Task<PersonDTO> CreatePerson(PersonDTO personDTO)
        {
            PersonTRA.ValidateYearsAndIdade(personDTO);

            Person person = new()
            {
                Id = personDTO.Id,
                Name = personDTO.Name,
                Age = personDTO.Age,
                Gender = personDTO.Gender,
                BirthDate = Convert.ToDateTime(personDTO.BirthDate)
            };

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return new PersonDTO
            {
                Id = person.Id,
                Name = person.Name,
                Age = person.Age,
                Gender = person.Gender,
                BirthDate = person.BirthDate.ToString("dd/MM/yyyy")
            };
        }

        public async Task<bool> DeleteAll()
        {
            List<Person> personList = await _context.Persons.ToListAsync();

            foreach (Person pessoa in personList)
            {
                _context.Persons.Remove(pessoa);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> DeletePerson(long id)
        {
            Person pessoa = await _context.Persons.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (pessoa == null)
                throw new Exception("Pessoa não encontrada");

            _context.Persons.Remove(pessoa);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<PersonDTO>> FindAllPerson()
        {
            List<Person> personList = await _context.Persons.ToListAsync();

            List<PersonDTO> personDTOList = new();

            foreach (Person person in personList)
            {
                PersonDTO personDTO = new()
                {
                    Id = person.Id,
                    Name = person.Name,
                    Age = person.Age,
                    Gender = person.Gender,
                    BirthDate = person.BirthDate.ToString("dd/MM/yyyy")
                };

                personDTOList.Add(personDTO);
            }

            return personDTOList;
        }

        public async Task<PersonDTO> FindPersonById(long id)
        {
            Person person = await _context.Persons.Where(x => x.Id == id).FirstOrDefaultAsync();

            if(person == null)
                throw new Exception($"Pessoa com o Id {id} não existe.");

            PersonDTO pessoaDTO = new()
            {
                Id  = person.Id,
                Name = person.Name,
                Age = person.Age,
                Gender = person.Gender,
                BirthDate = person.BirthDate.ToString("dd/MM/yyyy")
            };

            return pessoaDTO;
        }
        
        public async Task<PersonDTO> UpdatePerson(PersonDTO personDTO)
        {

            var id = _context.Persons.Where(x => x.Id == personDTO.Id).FirstOrDefault(); 

            if (id == null)
                throw new KeyNotFoundException("Id não existe na base de dados.");

            Person person = new()
            {
                Id = personDTO.Id,
                Name = personDTO.Name,
                Age = personDTO.Age,
                Gender = personDTO.Gender,
                BirthDate = Convert.ToDateTime(personDTO.BirthDate)
            };

            _context.Persons.Update(person);
            await _context.SaveChangesAsync();

            PersonDTO personsDTO = new()
            {
                Id = person.Id,
                Name = person.Name,
                Age = person.Age,
                Gender = person.Gender,
                BirthDate = person.BirthDate.ToString("dd/MM/yyyy")
            };

            return personsDTO;
        }
    }
}



