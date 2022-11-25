namespace Airashe.Core.Movement
{
    /// <summary>
    /// Состояние передвижения персонажа: движение отсутствует.
    /// </summary>
    public sealed class IdleCharacterMovementState : ACharacterMovementState
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
            entity.Height = entity.DefaultHeight;
            var rigidbody = entity.Rigidbody;
            if (rigidbody.velocity.magnitude == 0) return;

            rigidbody.AddForce(rigidbody.velocity * -5);
        }
    }
}
