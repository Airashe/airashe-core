using Airashe.Core.Common.Behaviours;
using Airashe.Core.Common.Utils;
using Airashe.Core.Controlable;
using Airashe.Core.Input;
using Airashe.Core.Movement;
using System;
using UnityEngine;

namespace Airashe.Core.Player
{
    /// <summary>
    /// Контроллер персонажа - от третьего лица.
    /// </summary>
    public class ThirdPersonCharacterController : MonoBehaviour, 
                                                  INotifiable<InputCommand>, 
                                                  IInitializable
    {
        /// <summary>
        /// Контролируемая сущность.
        /// </summary>
        [SerializeField] private Entity targetEntity;
        /// <summary>
        /// Камера.
        /// </summary>
        [SerializeField] private new ThirdPersonCamera camera;

        /// <summary>
        /// Последнее состояние передвижения, отправленное сущности.
        /// </summary>
        public ChangeMovementStateByInputCommandRequestData lastSendedMovementState;

        /// <summary>
        /// Камера управляется игроком.
        /// </summary>
        private bool manualCameraControl;
        [SerializeField] private float rotatingSpeed;

        #region Basic Methods

        private void Start() => Initialize();

        private void Update()
        {
            if (targetEntity is not null)
                camera.SetObservingPoint(targetEntity.transform.position + Vector3.up);
        }

        private void FixedUpdate()
        {
            ProcessManualCameraControl();
        }

        public void Notify(InputCommand eventData)
        {
            if ((eventData.LogicalName & CommandLogicalName.MovementCommands) == eventData.LogicalName)
            {
                ProcessMovementCommand(eventData);
                return;
            }

            if ((eventData.LogicalName & CommandLogicalName.CameraControls) == eventData.LogicalName)
                ProcessCameraCommand(eventData);
        }

        public void Initialize()
        {
            InputService.Instance.Subscribe(this);
            lastSendedMovementState = new ChangeMovementStateByInputCommandRequestData();
        }

        #endregion

        /// <summary>
        /// Обработка команды ввода движения.
        /// </summary>
        private void ProcessMovementCommand(InputCommand inputCommand)
        {
            var needChangeEntityState = lastSendedMovementState.Modify(inputCommand);
            if (needChangeEntityState)
            {
                if (!targetEntity.IsMoving)
                    targetEntity.LocalForwardDirection = camera.LookDirection;

                targetEntity.MovementController.RequestStateChange(targetEntity, lastSendedMovementState);
            }
        }

        /// <summary>
        /// Обработка команд, предназначающихся для камеры.
        /// </summary>
        /// <param name="inputCommand">Команда ввода.</param>
        private void ProcessCameraCommand(InputCommand inputCommand)
        {
            if (inputCommand.LogicalName == CommandLogicalName.MiddleMouse)
            {
                manualCameraControl = inputCommand.IsActive;
                return;
            }

            if (inputCommand.LogicalName == CommandLogicalName.MouseWheel)
            {
                camera.CameraDistance += inputCommand.ReadValue<Vector2>().y * Time.deltaTime * -1;
            }
        }

        /// <summary>
        /// Обработка ручного управления камерой.
        /// </summary>
        private void ProcessManualCameraControl()
        {
            if (!manualCameraControl) return;

            var cursorSpeed = InputService.Instance.GetCursorDelta() * Time.deltaTime * rotatingSpeed;
            camera.CameraAngles += cursorSpeed;
        }
    }
}
