using ListDePessoas.Data.DTO;
using System;

namespace ListDePessoas.Transaction
{
    public class PessoaTRA
    {
        public static void ValidateYearsAndIdade(PessoaDTO pessoa)
        {
            DateTime dateNow = DateTime.Now;
            TimeSpan validateYears = dateNow - Convert.ToDateTime(pessoa.DataNascimento);
            DateTime dateYers = Convert.ToDateTime(pessoa.DataNascimento);

            int idade;

            if (DateTime.Now.Month < dateYers.Month)
            {
                idade = (validateYears.Days / 365);

                if (idade < 18)
                    throw new Exception("Menor de idade");

                else if (idade != pessoa.Idade)
                    throw new Exception("Idade informada diferente da sua idade atual!");

                return;
            }
            else
                idade = (validateYears.Days / 365) + 1;

            if(idade < 18)
                throw new Exception("Menor de idade");

            else if(idade != pessoa.Idade)
                throw new Exception("Idade informada diferente da sua idade atual!");
        }
    }
}
