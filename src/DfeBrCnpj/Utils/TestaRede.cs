using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using DfeBrCnpj.Enums;

namespace DfeBrCnpj.Utils
{
    /// <summary>
    /// Classe para testes de Placa de rede e acesso a rede LAN/MAN/WAN
    /// </summary>
    public static class TestaRede
    {
        /// <summary>
        /// Testa se acesso a internet está ok.
        /// </summary>
        /// <param name="url">Url a ser acessada. Default http://google.com/generate_204</param>
        /// <returns>True ou False</returns>
        public static bool InternetOk(string url = "http://google.com/generate_204")
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead(url))
                    return true;
            }
            catch
            {
                return false;
            }

        }

        /// <summary>
        /// Valida Cnpj
        /// </summary>
        /// <param name="cnpj">Cnpj</param>
        /// <returns>Retorna true Cnpj Válido</returns>
        private static bool IsCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");

            if (cnpj.Length != 14)
            {
                return false;
            }
            else
            {
                var tempCnpj = cnpj.Substring(0, 12);

                var soma = 0;
                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

                var resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                var digito = resto.ToString();
                tempCnpj = tempCnpj + digito;

                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = digito + resto;
                return cnpj.EndsWith(digito);
            }
        }
    }
}