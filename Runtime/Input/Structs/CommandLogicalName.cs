using System;

namespace Airashe.Core.Input
{
    /// <summary>
    /// Логическое имя команды ввода.
    /// </summary>
    [Flags]
    public enum CommandLogicalName : int
    {
        /// <summary>
        /// Отсутствие команды ввода.
        /// </summary>
        None = 0, 

        /// <summary>
        /// Движение вперед.
        /// </summary>
        MoveForward = 0x1, 

        /// <summary>
        /// Движение назад.
        /// </summary>
        MoveBackward = 0x2, 

        /// <summary>
        /// Движение влево.
        /// </summary>
        MoveLeft = 0x4, 

        /// <summary>
        /// Движение вправо.
        /// </summary>
        MoveRight = 0x8, 

        /// <summary>
        /// Прыжок. 
        /// </summary>
        Jump = 0x10, 

        /// <summary>
        /// Красться.
        /// </summary>
        Crouch = 0x20, 

        /// <summary>
        /// Бег.
        /// </summary>
        Run = 0x40, 

        /// <summary>
        /// Левая кнопка мыши.
        /// </summary>
        RightMouse = 0x80, 

        /// <summary>
        /// Правая кнопка мыши.
        /// </summary>
        LeftMouse = 0x100, 

        /// <summary>
        /// Средняя кнопка мыши.
        /// </summary>
        MiddleMouse = 0x200, 

        /// <summary>
        /// Колесо мыши.
        /// </summary>
        MouseWheel = 0x300, 


        CameraControls = MiddleMouse | MouseWheel, 

        MovementCommands = MoveForward | MoveBackward | MoveLeft | MoveRight | Run | Crouch | Jump, 
    }
}
