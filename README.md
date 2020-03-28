# dfebrCNPJ
Biblioteca de consulta de CNPJ 


Muito simples de usar, onde apenas precisamos fazer o using DfeBrCnpj onde teremos a Entidades e servicos.


```Csharp

using DfeBrCnpj.Entidades;
using DfeBrCnpj.Servicos;
namespace DfeBrTestes 
{
    public static class TesteDfeBrCnpj
    {
        public static Cnpj Consulta() => ConsultaCnpj.Buscar("81437375000115"); // ESTE CNPJ É INVÁLIDO
    }
}
