using System;

namespace Airashe.Core.Movement
{
    /// <summary>
    /// Дескрипторы состояний.
    /// </summary>
    [Flags]
    public enum MovementStateDescriptor : byte
    {
        /// <summary>
        /// Отсутствие, каких либо дескрипторов.
        /// </summary>
        None = 0x0, 

        /// <summary>
        /// В этом состоянии объект считается передвигающимся.
        /// </summary>
        Moving = 0x1, 
    }
}
