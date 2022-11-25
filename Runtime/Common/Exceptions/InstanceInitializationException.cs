using System;

namespace Airashe.Core.Common.Exceptions
{
    /// <summary>
    /// Исключение, возникающее когда экземпляр неверно сконфигурирован, 
    /// из-за чего его работа невозможна.
    /// </summary>
    public sealed class InstanceInitializationException : Exception
    {
        /// <summary>
        /// Информация о возникшем исключении.
        /// </summary>
        public override string Message => messsage;

        /// <summary>
        /// Информация о возникшем исключении.
        /// </summary>
        private string messsage;
        public InstanceInitializationException(Type errorType, string reason)
        {
            messsage = $"{errorType?.Name ?? "Undefined"} initialization exception: {reason ?? "Undefined reason."}";
        }
    }
}
