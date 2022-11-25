using System;

namespace Airashe.Core.Movement
{
    /// <summary>
    /// Состояние передвижения, в котором может находится сущность.
    /// <see cref="IMoveable"/>.
    /// </summary>
    public abstract class AMovementState : IEquatable<AMovementState>
    {
        /// <summary>
        /// Список возможных состояний, в которые может перейти это состояние.
        /// </summary>
        public abstract AMovementState[] PossibleNewStates { get; }

        /// <summary>
        /// Дескрипторы состояния.
        /// </summary>
        public abstract MovementStateDescriptor Descriptors { get; }

        /// <summary>
        /// Идентификатор состояния.
        /// </summary>
        private Guid stateUid;

        /// <summary>
        /// Состояние передвижения, в котором может находитс сущность.
        /// </summary>
        public AMovementState ()
        {
            stateUid = Guid.NewGuid();
        }

        /// <summary>
        /// Обработать состояние для данных <paramref name="entity"/> за кадр.
        /// </summary>
        /// <param name="entity">Обрабатываемая сущность.</param>
        public abstract void Process(IMoveable entity);

        /// <summary>
        /// Можно ли из состояния перейти в состаяние <paramref name="possibleState"/>.
        /// </summary>
        /// <param name="possibleState">Новое возможное состояние.</param>
        /// <returns>
        /// Возвращает <see langword="true"/>, если переход возможен; 
        /// в противном случае возвращает <see langword="false"/>.
        /// </returns>
        public bool CanChangeTo(AMovementState possibleState)
        {
            var possibleStates = PossibleNewStates;

            for (byte i = 0; i < possibleStates.Length; i++)
                if (possibleStates[i].Equals(possibleState))
                    return true;
            return false;
        }

        /// <summary>
        /// Имеется ли в описании состояния <paramref name="descriptor"/>.
        /// </summary>
        /// <param name="descriptor">Описатель состояния.</param>
        /// <returns>
        /// Возвращает <see langword="true"/>, если состояние может быть описано дескриптором; 
        /// в противном случае возвращает <see langword="false"/>.
        /// </returns>
        public bool IsDescriptableBy(MovementStateDescriptor descriptor)
        {
            return (Descriptors & descriptor) == descriptor;
        }

        public bool Equals(AMovementState other)
        {
            if (other is null) return false;
            return stateUid == other.stateUid;
        }
    }
}
