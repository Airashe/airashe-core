using Airashe.Core.Common.Structs;

namespace Airashe.Core.Movement
{
    /// <summary>
    /// Состояние передвижения персонажа: Бег.
    /// </summary>
    public sealed class RunCharacterMovementState : ACharacterMovementState
    {
        public override AMovementState[] PossibleNewStates => new AMovementState[]
        {
            Idle, 
            Walk,
            Run, 
        };

        public override MovementStateDescriptor Descriptors => throw new System.NotImplementedException();

        public override void Process(IMoveable entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
