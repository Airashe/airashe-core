using Airashe.Core.Common.Behaviours;
using Airashe.Core.Common.Exceptions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Airashe.Core.Input
{
    /// <summary>
    /// Сервис ввода.
    /// </summary>
    public sealed class InputService : MonoSingleton<InputService>, INotifier<InputCommand>
    {
        /// <summary>
        /// Имя списка действий, с которым работает сервис ввода, по умолчанию.
        /// </summary>
        private const string DefaultActionMapName = "default";

        /// <summary>
        /// Конфигурация команд, с которой работает данный сервис ввода.
        /// </summary>
        [SerializeField] private InputActionAsset inputConfiguration;

        /// <summary>
        /// Текущая карта команд, с которой работает сервис.
        /// </summary>
        private InputActionMap currentActionMap;

        /// <summary>
        /// Делегат метода, для уведомления о произошедшем вводе.
        /// </summary>
        /// <param name="inputCommand">Данные команды.</param>
        private delegate void OnInputCommandEvent(InputCommand inputCommand);

        /// <summary>
        /// Событие, для хранения заинтересованных в вводе пользователя экземпляров.
        /// </summary>
        private event OnInputCommandEvent onInputCommandEvent;

        /// <summary>
        /// Позиция курсора, в последнем кадре.
        /// </summary>
        private Vector2 lastFrameCursorPosition;
        /// <summary>
        /// Изменение позиции курсора, с последнего кадра.
        /// </summary>
        private Vector2 cursorDelta;

        private void Update()
        {
            CalculateCursorDelta();
        }

        /// <summary>
        /// Высчитать изменение позиции курсора, с прошлого кадра.
        /// </summary>
        private void CalculateCursorDelta()
        {
            var currentPosition = GetCursorPosition();
            cursorDelta = lastFrameCursorPosition - currentPosition;
            lastFrameCursorPosition = currentPosition;
        }

        protected override void OnAwake()
        {
            ServiceInitialize();
        }

        /// <summary>
        /// Получить позицию курсора на экране.
        /// </summary>
        /// <returns>
        /// Возвращает текущую позицию курсора на экране.
        /// </returns>
        public Vector2 GetCursorPosition()
        {
            return Mouse.current.position.ReadValue();
        }

        /// <summary>
        /// Изменение позиции курсора, с прошлого кадра.
        /// </summary>
        /// <param name="accurate">Передавать точное изменение (с погрешностями).</param>
        /// <returns>
        /// Возвращает разницу между позицией курсора в прошлом кадре и в текущем кадре.
        /// </returns>
        public Vector2 GetCursorDelta(bool accurate = false)
        {
            if (accurate)
                return cursorDelta;

            var result = cursorDelta;
            result.x = Mathf.Abs(result.x) > 1 ? result.x : 0;
            result.y = Mathf.Abs(result.y) > 1 ? result.y : 0;
            return result;
        }

        #region Input Logic

        /// <summary>
        /// Инициализация сервиса.
        /// </summary>
        /// <exception cref="InstanceInitializationException">
        /// Выбрасывает исключение, если сервис сконфигурирован неверно.
        /// </exception>
        private void ServiceInitialize()
        {
            if (inputConfiguration is null) throw new InstanceInitializationException(GetType(), $"{nameof(inputConfiguration)} was null");
            ActivateInputActionAsset(DefaultActionMapName);
        }

        /// <summary>
        /// Прочитать содержимое <see cref="InputActionAsset"/> и 
        /// активировать его содержимое.
        /// </summary>
        private void ActivateInputActionAsset(string inputActionMap)
        {
            DeactivateActionMap(currentActionMap);

            currentActionMap = inputConfiguration.FindActionMap(inputActionMap) ?? new InputActionMap("default");
            ActivateActionMap(currentActionMap);
        }

        /// <summary>
        /// Деактивировать карту команд.
        /// </summary>
        /// <param name="actionMap">Экземпляр карты команд.</param>
        private void DeactivateActionMap(InputActionMap actionMap)
        {
            if (actionMap is null) return;

            foreach(var action in actionMap.actions)
                MuteAction(action);
        }

        /// <summary>
        /// Активировать карту команд.
        /// </summary>
        /// <param name="actionMap">Карта команд.</param>
        private void ActivateActionMap(InputActionMap actionMap)
        {
            if (actionMap is null) return;

            foreach (var action in actionMap.actions)
                ListenToAction(action);
        }

        /// <summary>
        /// Заставить сервис ввода слушать команду <paramref name="action"/>.
        /// </summary>
        /// <param name="action">Команда для прослушивания.</param>
        private void ListenToAction(InputAction action)
        {
            action.Enable();
            action.started += InputCommandStarted;
            action.canceled += InputCommandStarted;
        }

        /// <summary>
        /// Перестать прослушивать команду ввода <paramref name="action"/>.
        /// </summary>
        /// <param name="action">Команда для прослушивания.</param>
        private void MuteAction(InputAction action)
        {
            action.Disable();
            action.started -= InputCommandStarted;
        }

        /// <summary>
        /// Метод, вызываемый когда событие ввода началось.
        /// </summary>
        /// <param name="obj">Данные события ввода.</param>
        private void InputCommandStarted(InputAction.CallbackContext obj)
        {
            var inputCommand = new InputCommand(obj);

            var listenersToNotify = onInputCommandEvent;
            listenersToNotify?.Invoke(inputCommand);
        }

        #endregion

        #region Notifier Realisation

        public void Subscribe(INotifiable<InputCommand> notifiable)
        {
            if (notifiable is null) return;
            onInputCommandEvent += notifiable.Notify;
        }

        public void Unsubscribe(INotifiable<InputCommand> notifiable)
        {
            if (notifiable is null) return;
            onInputCommandEvent -= notifiable.Notify;
        }

        #endregion
    }
}