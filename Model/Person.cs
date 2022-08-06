using APIPerson.Model.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace APIPerson.Model
{
    public class Person : BaseEntity
    {
        [Required(ErrorMessage = "Nome obrigatório"), MaxLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Idade obrigatório"), MaxLength(2), MinLength(2)]
        public int Age { get; set; }

        public string Gender { get; set; }

        public DateTime BirthDate { get; set; }
    }
}
