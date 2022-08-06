using APIPerson.Data.DTO;
using System;

namespace APIPerson.Transaction
{
    public class PersonTRA
    {
        public static void ValidateYearsAndIdade(PersonDTO pessoa)
        {
            DateTime dateNow = DateTime.Now;
            DateTime dateYers = Convert.ToDateTime(pessoa.BirthDate);

            long age = dateNow.Year - dateYers.Year - 1;

            if (DateTime.Now.Month >= dateYers.Month)
            {
                age = dateNow.Year - dateYers.Year;

                if (age < 18)
                    throw new Exception("Menor de idade");

                else if (age != pessoa.Age)
                    throw new Exception("Idade informada diferente da sua idade atual!");

                return;
            }

            if (age < 18)
                throw new Exception("Menor de idade");

            else if (age != pessoa.Age)
                throw new Exception("Idade informada diferente da sua idade atual!");
        }
    }
}
