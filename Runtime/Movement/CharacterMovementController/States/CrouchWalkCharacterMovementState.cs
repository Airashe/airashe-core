namespace Airashe.Core.Movement
{
    /// <summary>
    /// Состояние передвижения персонажа: движение в присяди.
    /// </summary>
    public sealed class CrouchWalkCharacterMovementState : ACharacterMovementState
    {
        public override AMovementState[] PossibleNewStates => new AMovementState[]
        {
            Idle,
            CrouchIdle,
            Walk,
            CrouchWalk, 
            Run,
        };

        public override MovementStateDescriptor Descriptors => MovementStateDescriptor.Moving;

        public override void Process(IMoveable entity)
        {
            entity.Height = entity.CrouchHeight;
            var rigidbody = entity.Rigidbody;

            bool reachedMaxSpeed = rigidbody.velocity.magnitude > entity.CrouchSpeeds.MaxValue;
            if (reachedMaxSpeed) return;

            var movementDirection = entity.ShiftVectorByLocalForward(entity.MovementDirection);
            rigidbody.AddForce(movementDirection, UnityEngine.ForceMode.VelocityChange);
        }
    }
}
