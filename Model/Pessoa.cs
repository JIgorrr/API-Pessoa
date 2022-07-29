using ListDePessoas.Model.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace ListDePessoas.Model
{
    public class Pessoa : BaseEntity
    {
        [Required(ErrorMessage = "Nome obrigatório"), MaxLength(255)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Idade obrigatório"), MaxLength(2), MinLength(2)]
        public int Idade { get; set; }

        public string Sexo { get; set; }

        public DateTime DataNascimento { get; set; }
    }
}
