using System.Net;

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
    }
}