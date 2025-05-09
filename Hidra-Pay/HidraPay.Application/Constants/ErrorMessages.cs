﻿
namespace HidraPay.Application.Constants
{
    /// <summary>
    /// Mensagens de erro utilizadas pela camada de aplicação.
    /// </summary>
    public static class ErrorMessages
    {
        /// <summary>
        /// Formato para quando um método de pagamento não é suportado.
        /// </summary>
        public const string MethodNotSupported = "Método {0} não suportado.";

        /// <summary>
        /// Formato para quando um método de pagamento não é encontrado.
        /// </summary>

        public const string TransactionNotFound = "Transação '{0}' não encontrada.";
    }
}
