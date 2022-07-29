using ListDePessoas.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ListDePessoas.Repository
{
    public interface IPessoaRepository
    {
        Task<List<PessoaDTO>> FindAllPessoa();

        Task<PessoaDTO> FindPessoaById(long id);

        Task<PessoaDTO> CreatePessoa(PessoaDTO pessoa);

        Task<PessoaDTO> UpdatePessoa(PessoaDTO pessoa);

        Task<bool> DeletePessoa(long id);

        Task<bool> DeleteAll();
    }
}
