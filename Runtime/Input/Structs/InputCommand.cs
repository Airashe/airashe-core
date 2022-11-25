using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Airashe.Core.Input
{
    /// <summary>
    /// Команда ввода.
    /// </summary>
    /// Перечисление, к которому приводится команда.
    /// </typeparam>
    public struct InputCommand
    {
        /// <summary>
        /// Активна ли команда.
        /// </summary>
        public bool IsActive => context.phase == InputActionPhase.Started;

        /// <summary>
        /// Логическое имя команды.
        /// </summary>
        public CommandLogicalName LogicalName => logicalName;

        private CommandLogicalName logicalName;
        private InputAction.CallbackContext context;


        /// <summary>
        /// Команда ввода.
        /// </summary>
        /// <param name="callbackContext">Контекст ввода Unity Input System</param>
        public InputCommand(InputAction.CallbackContext callbackContext)
        {
            Enum.TryParse(callbackContext.action.name, out logicalName);
            context = callbackContext;
        }

        /// <summary>
        /// Получить значение <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Тип значения.</typeparam>
        /// <returns>
        /// Возвращает значение <typeparamref name="T"/> команды.
        /// </returns>
        public T ReadValue<T>() where T : struct => context.ReadValue<T>();

        public override string ToString()
        {
            return $"[{logicalName} - {context.phase}]";
        }
    }
}
