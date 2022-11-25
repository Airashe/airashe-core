namespace Airashe.Core.Movement
{
    /// <summary>
    /// Состояние передвижения персонажа.
    /// </summary>
    public abstract class ACharacterMovementState : AMovementState
    {
        /// <summary>
        /// Отсутствие поведения.
        /// </summary>
        public static readonly ACharacterMovementState Idle = new IdleCharacterMovementState();

        /// <summary>
        /// Присед + Отсутствие поведения.
        /// </summary>
        public static readonly ACharacterMovementState CrouchIdle = new CrouchIdleCharacterMovementState();

        /// <summary>
        /// Ходьба.
        /// </summary>
        public static readonly ACharacterMovementState Walk = new WalkCharacterMovementState();

        /// <summary>
        /// Присед + Ходьба.
        /// </summary>
        public static readonly ACharacterMovementState CrouchWalk = new CrouchWalkCharacterMovementState();

        /// <summary>
        /// Бег.
        /// </summary>
        public static readonly ACharacterMovementState Run = new IdleCharacterMovementState();

        /// <summary>
        /// Начало прыжка.
        /// </summary>
        public static readonly ACharacterMovementState JumpBegin = new IdleCharacterMovementState();

        /// <summary>
        /// Падение.
        /// </summary>
        public static readonly ACharacterMovementState Falling = new IdleCharacterMovementState();

        /// <summary>
        /// Подкат.
        /// </summary>
        public static readonly ACharacterMovementState Sliding = new IdleCharacterMovementState();

        /// <summary>
        /// Бег по стене.
        /// </summary>
        public static readonly ACharacterMovementState WallRun = new IdleCharacterMovementState();

        /// <summary>
        /// Вскарабкивание.
        /// </summary>
        public static readonly ACharacterMovementState WallClimb = new IdleCharacterMovementState();
    }
}
