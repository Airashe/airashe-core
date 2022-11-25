using Airashe.Core.Common.Behaviours;
using Airashe.Core.Common.Structs;
using Airashe.Core.Movement;
using UnityEngine;

namespace Airashe.Core.Controlable
{
    /// <summary>
    /// Внутриигровой объект.
    /// </summary>
    public sealed class Entity : MonoBehaviour,
                                 IInitializable,
                                 IMoveable
    {
        public AMovementController MovementController { get => movementController; set => movementController = value; }
        public AMovementState MovementState 
        { 
            get => movementState; 
            set
            {
                PreviousMovementState = movementState;
                movementState = value ?? MovementController.DefaultState;
                movementStateName = movementState?.GetType().Name;
            }
        }
        public AMovementState PreviousMovementState
        {
            get => previousMovementState;
            private set
            {
                previousMovementState = value;
                previousMovementStateName = previousMovementState?.GetType().Name;
            }
        }

        public Vector3 MovementDirection
        {
            get => localMovementDirection;
            set => localMovementDirection = value;
        }

        public Vector3 LocalForwardDirection { get; set; }

        public bool IsMoving => MovementController.IsMoving(this);

        public BorderedFloat WalkSpeeds
        {
            get => walkSpeeds;
            set => walkSpeeds = value;
        }

        public BorderedFloat RunSpeeds
        {
            get => runSpeeds;
            set => runSpeeds = value;
        }

        public BorderedFloat CrouchSpeeds
        {
            get => crouchSpeeds;
            set => crouchSpeeds = value;
        }

        public Rigidbody Rigidbody => rigidbodyRef;

        public float Height
        {
            get => height;
            set
            {
                if (Mathf.Abs(height - value) <= 0.01f) return;

                height = value;
                ((CapsuleCollider)colliderRef).height = height;
            }
        }

        public float DefaultHeight => defaultHeight;
        public float CrouchHeight => crouchHeight;

        /// <summary>
        /// Контроллер обрабатывающий перемещения экземпляра.
        /// </summary>
        [Header("Movement")]
        [SerializeField] private AMovementController movementController;
        private AMovementState movementState;
        private AMovementState previousMovementState;
        private Vector3 localMovementDirection;
        [SerializeField] private BorderedFloat walkSpeeds;
        [SerializeField] private BorderedFloat runSpeeds;
        [SerializeField] private BorderedFloat crouchSpeeds;

        /// <summary>
        /// Высота сущности в покое.
        /// </summary>
        [Header("Properties")]
        [SerializeField] private float defaultHeight;
        /// <summary>
        /// Высота сущности в присяди.
        /// </summary>
        [SerializeField] private float crouchHeight;

        /// <summary>
        /// Высота сущности.
        /// </summary>
        [SerializeField] private float height;

        /// <summary>
        /// Компонент физики сущности.
        /// </summary>
        [Header("Components")]
        [SerializeField] private Rigidbody rigidbodyRef;
        /// <summary>
        /// Коллайдер сущности.
        /// </summary>
        [SerializeField] private Collider colliderRef;


        private string movementStateName;
        private string previousMovementStateName;

        private void Start() => Initialize();

        public void Initialize()
        {
            MovementController.SetEntityToControl(this);
            if (rigidbodyRef == null)
                rigidbodyRef = GetComponent<Rigidbody>();
            if (colliderRef == null)
                colliderRef = GetComponent<Collider>();
        }
    }
}
