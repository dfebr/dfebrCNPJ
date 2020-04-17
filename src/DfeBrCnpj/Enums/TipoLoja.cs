using System.ComponentModel;

namespace DfeBrCnpj.Enums
{
    public enum TipoLoja
    {
        [Description("Matriz")]
        Matriz = 1,
        [Description("Filial")]
        Filial = 2,
        [Description("Depósito Central")]
        DepositoCentral = 3,
        [Description("Depósito")]
        Deposito = 4
    }
}