using System.Net;
using System.Runtime.Serialization.Json;
using DfeBrCnpj.Entidades;
using DfeBrCnpj.Utils;

namespace DfeBrCnpj.Servicos
{
    public static class ConsultaCnpj
    {
        /// <summary>
        /// Pesquisa Cnpj Site da Receita
        /// </summary>
        /// <param name="aCnpj">Cnpj a ser pesquisado. Somente Números</param>
        /// <param name="tempoResposta">Define o tempo que irá aguardar resposta do webservice em milesegundos. Default 5000 (5 Segundos)</param>
        /// <returns>Objeto Cnpj Preenchido com os dados</returns>
        public static Cnpj Buscar(string aCnpj, int tempoResposta = 5000)
        {
            var cnpj = new Cnpj {cnpj = aCnpj};
            // VALIDACAO MÍNIMA - EVITAR CONSULTA DESNECESSÁRIA AO SITE
            if (!cnpj.ValidaCnpj()) return null;
            HttpWebResponse response = null;

            var auxUri = "https://www.receitaws.com.br/v1/cnpj/" + aCnpj;
            try
            {
                if (TestaRede.InternetOk())
                {
                    var request = (HttpWebRequest)WebRequest.Create(auxUri);
                    request.Timeout = tempoResposta;
                    request.Method = WebRequestMethods.Http.Get;
                    request.Accept = "application/json";


                    response = (HttpWebResponse)request.GetResponse();

                    var responsestream = response.GetResponseStream();
                    if (responsestream != null)
                    {
                        var cnpjSer = new DataContractJsonSerializer(typeof(Cnpj));
                        cnpj = (Cnpj)cnpjSer.ReadObject(responsestream);

                        var ser = new DataContractJsonSerializer(typeof(Cnpj));
                    }
                }
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    response = (HttpWebResponse)e.Response;
                }
                else
                {
                    cnpj.Erros.Add($"Erro ao tentar consulta cnpj. Error{e.Message}");
                }
            }
            finally
            {
                response?.Close();
            }
            return cnpj.Valido() ? cnpj : null;
        }

        private static bool ValidaCnpj(this Cnpj cnpj)
        {
            // VALIDAÇÃO MÍNIMA BASICA.
            // EVITA CONSULTA DESNECESSÁRIA NO SITE DA RECEITA (MUITAS CONSULTAS PODEM LEVAR O BLOQUEIO DO IP
            if (IsCnpj(cnpj.cnpj) == false)
                cnpj.Erros.Add("CNPJ não informado corretamente\n");
            return cnpj.Valido();
        }

        /// <summary>
        /// Valida Cnpj
        /// </summary>
        /// <param name="cnpj">Cnpj</param>
        /// <returns>Retorna true Cnpj Válido</returns>
        private static bool IsCnpj(string cnpj)
        {
            var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

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
                for (var i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

                var resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                var digito = resto.ToString();
                tempCnpj += digito;

                soma = 0;
                for (var i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

                resto = (soma % 11);
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito += resto;
                return cnpj.EndsWith(digito);
            }
        }

    }

}