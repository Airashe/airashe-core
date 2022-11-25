using Airashe.Core.Input;
using UnityEngine;

namespace Airashe.Core.Movement
{
    /// <summary>
    /// Запрос на изменение состояние персонажа, по запросу игрока.
    /// </summary>
    public sealed class ChangeMovementStateByInputCommandRequestData : AMovementStateChageRequestData
    {
        /// <summary>
        /// Направление движения.
        /// </summary>
        public Vector3 MovementDirection { get; private set; }

        /// <summary>
        /// Модификаторы движения.
        /// </summary>
        public MovementModifier Modifiers { get; private set; }

        /// <summary>
        /// Запрос на изменение состояние персонажа, по запросу игрока.
        /// </summary>
        public ChangeMovementStateByInputCommandRequestData()
        {
            MovementDirection = Vector3.zero;
        }

        /// <summary>
        /// Активен ли модификатор передвижения.
        /// </summary>
        /// <param name="modifier">Проверяемый модификатор.</param>
        /// <returns>
        /// Возвращает <see langword="true"/>, если модификатор активен;
        /// в противном случае возвращает <see langword="false"/>.
        /// </returns>
        public bool IsModifierActive(MovementModifier modifier)
        {
            return (Modifiers & modifier) == modifier;
        }

        /// <summary>
        /// Команда, которая вызвала запрос изменения.
        /// (Считай последняя нажатая клавиша).
        /// Если команда вызвала изменение деактивацией - значение <see cref="CommandLogicalName.None"/>.
        /// </summary>
        public CommandLogicalName RequestByActiveCommand;

        /// <summary>
        /// Модифицировать запрос данными новой команды.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <returns>
        /// Возвращает <see langword="false"/>, если запрос не был модифицирован; 
        /// в противном случае возвращает <see langword="true"/>.
        /// </returns>
        public bool Modify(InputCommand command)
        {
            var newDirection = MovementDirection;
            bool isActive = command.IsActive;
            bool isChanged = false;
    
            switch (command.LogicalName)
            {
                case CommandLogicalName.MoveForward:
                    newDirection.z += isActive ? 1 : -1;
                    break;
                case CommandLogicalName.MoveBackward:
                    newDirection.z -= isActive ? 1 : -1;
                    break;
                case CommandLogicalName.MoveLeft:
                    newDirection.x -= isActive ? 1 : -1;
                    break;
                case CommandLogicalName.MoveRight:
                    newDirection.x += isActive ? 1 : -1;
                    break;

                case CommandLogicalName.Crouch:
                    isChanged |= SetModifier(MovementModifier.Crouch, isActive);
                    break;
                case CommandLogicalName.Run:
                    isChanged |= SetModifier(MovementModifier.Run, isActive);
                    break;
                case CommandLogicalName.Jump:
                    isChanged |= SetModifier(MovementModifier.Jump, isActive);
                    break;
            }

            RequestByActiveCommand = isActive ? command.LogicalName : CommandLogicalName.None;

            var previousDirection = MovementDirection;
            MovementDirection = newDirection;
            isChanged |= previousDirection != newDirection;
            return isChanged;
        }

        /// <summary>
        /// Установить или снять модификатор.
        /// </summary>
        /// <param name="modifier">Модифкатор.</param>
        /// <param name="isActive">Активен ли модификатор.</param>
        /// <returns>
        /// Возвращает <see langword="true"/>, если состояние изменилось;
        /// в противном случае возвращет <see langword="false"/>.
        /// </returns>
        private bool SetModifier(MovementModifier modifier, bool isActive)
        {
            bool isChanged = IsModifierActive(modifier) != isActive;
            if (isActive)
            {
                Modifiers |= modifier;
                return isChanged;
            }

            Modifiers &= ~modifier;
            return isChanged;
        }
    }
}
