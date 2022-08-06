using System.ComponentModel.DataAnnotations;

namespace APIPerson.Model.Base
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
    }
}
