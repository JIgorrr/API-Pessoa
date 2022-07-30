using ListDePessoas.Data.DTO;
using ListDePessoas.Model;
using ListDePessoas.Model.Context;
using ListDePessoas.Transaction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListDePessoas.Repository
{
    public class PessoaReporitory : IPessoaRepository
    {
        private readonly ContextPessoa _context;

        public PessoaReporitory(ContextPessoa context)
        {
            _context = context;
        }

        public async Task<PessoaDTO> CreatePessoa(PessoaDTO person)
        {
            PessoaTRA.ValidateYearsAndIdade(person);

            Pessoa pessoa = new()
            {
                Id = person.Id,
                Nome = person.Nome,
                Idade = person.Idade,
                Sexo = person.Sexo,
                DataNascimento = Convert.ToDateTime(person.DataNascimento)
            };

            _context.Perssoas.Add(pessoa);
            await _context.SaveChangesAsync();

            return new PessoaDTO
            {
                Id = pessoa.Id,
                Nome = pessoa.Nome,
                Idade = pessoa.Idade,
                Sexo = pessoa.Sexo,
                DataNascimento = pessoa.DataNascimento.ToString("dd/MM/yyyy")
            };
        }

        public async Task<bool> DeleteAll()
        {
            List<Pessoa> pessoaList = await _context.Perssoas.ToListAsync();

            foreach (Pessoa pessoa in pessoaList)
            {
                _context.Perssoas.Remove(pessoa);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> DeletePessoa(long id)
        {
            Pessoa pessoa = await _context.Perssoas.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (pessoa == null)
                throw new Exception("Pessoa não encontrada");

            _context.Perssoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<PessoaDTO>> FindAllPessoa()
        {
            List<Pessoa> pessoaList = await _context.Perssoas.ToListAsync();

            List<PessoaDTO> pessoaDTOList = new();

            foreach (Pessoa pessoa in pessoaList)
            {
                PessoaDTO pessoaDTO = new()
                {
                    Id = pessoa.Id,
                    Nome = pessoa.Nome,
                    Idade = pessoa.Idade,
                    Sexo = pessoa.Sexo,
                    DataNascimento = pessoa.DataNascimento.ToString("dd/MM/yyyy")
                };

                pessoaDTOList.Add(pessoaDTO);
            }

            return pessoaDTOList;
        }

        public async Task<PessoaDTO> FindPessoaById(long id)
        {
            Pessoa pessoa = await _context.Perssoas.Where(x => x.Id == id).FirstOrDefaultAsync();

            if(pessoa == null)
                throw new Exception($"Pessoa com o Id {id} não existe.");

            PessoaDTO pessoaDTO = new()
            {
                Id  = pessoa.Id,
                Nome = pessoa.Nome,
                Idade = pessoa.Idade,
                Sexo = pessoa.Sexo,
                DataNascimento = pessoa.DataNascimento.ToString("dd/MM/yyyy")
            };

            return pessoaDTO;
        }

        public async Task<PessoaDTO> UpdatePessoa(PessoaDTO person)
        {
            var id = _context.Perssoas.Where(x => x.Id == person.Id).FirstOrDefault();

            if (id == null)
                throw new KeyNotFoundException("Id não existe na base de dados.");

            Pessoa pessoa = new()
            {
                Id = person.Id,
                Nome = person.Nome,
                Idade = person.Idade,
                Sexo = person.Sexo,
                DataNascimento = Convert.ToDateTime(person.DataNascimento)
            };

            _context.Perssoas.Update(pessoa);
            await _context.SaveChangesAsync();

            PessoaDTO pessoaDTO = new()
            {
                Nome = pessoa.Nome,
                Idade = pessoa.Idade,
                Sexo = pessoa.Sexo,
                DataNascimento = pessoa.DataNascimento.ToString("dd/MM/yyyy")
            };

            return pessoaDTO;
        }
    }
}



