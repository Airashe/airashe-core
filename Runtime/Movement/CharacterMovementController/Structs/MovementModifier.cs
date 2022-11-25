namespace Airashe.Core.Movement
{
    /// <summary>
    /// Модификаторы передвижения.
    /// </summary>
    [System.Flags]
    public enum MovementModifier : byte
    {
        /// <summary>
        /// Отсутствие модификаторов.
        /// </summary>
        None = 0x00,
        
        /// <summary>
        /// Модификатор присяда.
        /// </summary>
        Crouch = 0x01,

        /// <summary>
        /// Модификатор бега.
        /// </summary>
        Run = 0x02, 

        /// <summary>
        /// Модификатор прыжка.
        /// </summary>
        Jump = 0x04,
    }
}
