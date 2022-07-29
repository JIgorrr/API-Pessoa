using System.ComponentModel.DataAnnotations;

namespace ListDePessoas.Model.Base
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
