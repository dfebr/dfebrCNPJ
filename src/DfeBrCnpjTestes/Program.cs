using System;
using DfeBrCnpj.Entidades;
using DfeBrCnpj.Servicos;

namespace DfeBrCnpjTestes
{
    class Program
    {
        static void Main(string[] args)
        {

            //Não aceita cnpj vazio ou nulo
            Console.WriteLine("------| TESTANDO CNPJ VAZIO |-------");
            Console.WriteLine(ConsultaCnpj.Buscar("") == null ? "Cnpj Nulo/Vazio Não pode ser consultado" : "Tudo OK.");

            Console.WriteLine("------| TESTANDO CNPJ VÁLIDO |-------");
            Console.WriteLine("");
            Cnpj cnpjConsultado = ConsultaCnpj.Buscar("01862236000108");
            if (cnpjConsultado.Valido())
            {
                Console.WriteLine($"------| CONSULTA CNPJ: {cnpjConsultado.cnpj} - RAZÃO SOCIAL: {cnpjConsultado.nome} |-------");
                foreach (var propriedade in cnpjConsultado.GetType().GetProperties())
                {
                    if (!propriedade.PropertyType.Name.Contains("List")) // Não lista as propriedades que são listas, como é somente teste, não há necessidade.
                        Console.WriteLine($"{propriedade.Name.ToUpper()}: {propriedade.GetValue(cnpjConsultado, null)}");
                }
            }
            Console.WriteLine("");
            Console.WriteLine("------| TESTANDO CNPJ INVÁLIDO |-------");
            cnpjConsultado = ConsultaCnpj.Buscar("81437375000115");
            Console.WriteLine(cnpjConsultado == null ? "CNPJ INVÁLIDO" : $"CNPJ CONSULTADO: {cnpjConsultado.cnpj} - RAZAO: {cnpjConsultado.nome}");
            Console.WriteLine("");
            Console.WriteLine("Testes Finalizados!");
        }
    }


}
