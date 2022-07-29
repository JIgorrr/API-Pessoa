using ListDePessoas.Model.Base;
using System;

namespace ListDePessoas.Data.DTO
{
    public class PessoaDTO : BaseEntity
    {
        public string Nome { get; set; }

        public int Idade { get; set; }

        public string Sexo { get; set; }

        public string DataNascimento { get; set; }
    }
}
