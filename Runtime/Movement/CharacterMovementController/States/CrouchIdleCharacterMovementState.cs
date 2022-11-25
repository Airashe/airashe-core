namespace Airashe.Core.Movement
{
    public sealed class CrouchIdleCharacterMovementState : ACharacterMovementState
    {
        public override AMovementState[] PossibleNewStates => new AMovementState[]
        {
            Idle,
            CrouchIdle,
            Walk,
            Run,
        };

        public override MovementStateDescriptor Descriptors => MovementStateDescriptor.None;

        public override void Process(IMoveable entity)
        {
            entity.Height = entity.CrouchHeight;
            var rigidbody = entity.Rigidbody;
            if (rigidbody.velocity.magnitude == 0) return;

            rigidbody.AddForce(rigidbody.velocity * -5);
        }
    }
}
