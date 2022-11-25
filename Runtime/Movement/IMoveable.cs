using Airashe.Core.Common.Structs;
using Airashe.Core.Common.Utils;
using UnityEngine;

namespace Airashe.Core.Movement
{
    /// <summary>
    /// Интерфейс объекта, способного к передвижению в пространстве.
    /// </summary>
    public interface IMoveable
    {
        /// <summary>
        /// Контроллер, обабатывающий логику перемещения.
        /// </summary>
        public AMovementController MovementController { get; set; }

        /// <summary>
        /// Состояние перевижения.
        /// </summary>
        public AMovementState MovementState { get; set; }

        /// <summary>
        /// Предыдущее состояние передвижения.
        /// </summary>
        public AMovementState PreviousMovementState { get; }

        /// <summary>
        /// Направление движения в локальных координатах.
        /// </summary>
        public Vector3 MovementDirection { get; set; }

        /// <summary>
        /// Направление: вперед, для объекта.
        /// </summary>
        public Vector3 LocalForwardDirection { get; set; }

        /// <summary>
        /// Скорость передвижения пешком.
        /// </summary>
        public BorderedFloat WalkSpeeds { get; set; }

        /// <summary>
        /// Скорость передвижения бегом.
        /// </summary>
        public BorderedFloat RunSpeeds { get; set; }

        /// <summary>
        /// Скорость передвижения в присяди.
        /// </summary>
        public BorderedFloat CrouchSpeeds { get; set; }

        /// <summary>
        /// Компонент позиционирования.
        /// </summary>
        public Rigidbody Rigidbody { get; }

        /// <summary>
        /// Высота сущности.
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// Высота сущности по умолчанию.
        /// </summary>
        public float DefaultHeight { get; }

        /// <summary>
        /// Высота сущности (в присяди).
        /// </summary>
        public float CrouchHeight { get; }

        /// <summary>
        /// Передвигается ли объект, в данный момент.
        /// </summary>
        public bool IsMoving { get; }

        /// <summary>
        /// Перевести вектор в систему координат, где вперед - локальный вперед <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <param name="vector">Оригинальный вектор.</param>
        /// <returns>
        /// Возвращает вектор в координатной системе сущности.
        /// </returns>
        public Vector3 ShiftVectorByLocalForward(Vector3 vector)
        {
            var leftOrRightValue = VectorUtility.AngleDir(Vector3.forward, LocalForwardDirection);

            var rotationAngle = Vector3.Angle(Vector3.forward, LocalForwardDirection);
            if (leftOrRightValue == -1)
                rotationAngle = 360 - rotationAngle;

            var rotatedVector = Quaternion.AngleAxis(rotationAngle, Vector3.up) * vector;
            return rotatedVector;
        }
    }
}
