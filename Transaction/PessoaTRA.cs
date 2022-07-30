using ListDePessoas.Data.DTO;
using System;

namespace ListDePessoas.Transaction
{
    public class PessoaTRA
    {
        public static void ValidateYearsAndIdade(PessoaDTO pessoa)
        {
            DateTime dateNow = DateTime.Now;
            DateTime dateYers = Convert.ToDateTime(pessoa.DataNascimento);

            long idade = dateNow.Year - dateYers.Year - 1;

            if (DateTime.Now.Month >= dateYers.Month)
            {
                idade = dateNow.Year - dateYers.Year;

                if (idade < 18)
                    throw new Exception("Menor de idade");

                else if (idade != pessoa.Idade)
                    throw new Exception("Idade informada diferente da sua idade atual!");

                return;
            }

            if (idade < 18)
                throw new Exception("Menor de idade");

            else if (idade != pessoa.Idade)
                throw new Exception("Idade informada diferente da sua idade atual!");
        }
    }
}
