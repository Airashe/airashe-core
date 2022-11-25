using Airashe.Core.Common.Structs;

namespace Airashe.Core.Movement
{
    /// <summary>
    /// Состояние передвижения персонажа: ходьба.
    /// </summary>
    public sealed class WalkCharacterMovementState : ACharacterMovementState
    {
        public override AMovementState[] PossibleNewStates => new AMovementState[]
        {
            Idle,
            CrouchIdle, 
            Walk,
            Run,
        };

        public override MovementStateDescriptor Descriptors => MovementStateDescriptor.Moving;

        public override void Process(IMoveable entity)
        {
            entity.Height = entity.DefaultHeight;
            var rigidbody = entity.Rigidbody;

            bool reachedMaxSpeed = rigidbody.velocity.magnitude > entity.WalkSpeeds.MaxValue;
            if (reachedMaxSpeed) return;

            var movementDirection = entity.ShiftVectorByLocalForward(entity.MovementDirection);
            rigidbody.AddForce(movementDirection, UnityEngine.ForceMode.VelocityChange);
        }
    }
}
