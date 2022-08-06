using APIPerson.Model.Base;

namespace APIPerson.Data.DTO
{
    public class PersonDTO : BaseEntity
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Gender { get; set; }

        public string BirthDate { get; set; }
    }
}
