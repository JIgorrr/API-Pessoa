using APIPerson.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIPerson.Repository
{
    public interface IPersonRepository
    {
        Task<List<PersonDTO>> FindAllPerson();

        Task<PersonDTO> FindPersonById(long id);

        Task<PersonDTO> CreatePerson(PersonDTO pessoa);

        Task<PersonDTO> UpdatePerson(PersonDTO pessoa);

        Task<bool> DeletePerson(long id);

        Task<bool> DeleteAll();
    }
}
