using Airashe.Core.Common.Behaviours;
using System.Collections.Generic;
using UnityEngine;

namespace Airashe.Core.Movement
{
    /// <summary>
    /// Абстрактный контроллер передвижения внутриигрового объекта.
    /// </summary>
    public abstract class AMovementController : MonoBehaviour, IInitializable
    {
        /// <summary>
        /// Получить первоначальное состояние, в котором должна находится сущность.
        /// </summary>
        /// <returns>
        /// Возвращает первоначальное состояние сущности.
        /// </returns>
        public abstract AMovementState DefaultState { get; }

        /// <summary>
        /// Контролируемые сущности.
        /// </summary>
        private List<IMoveable> controlledEntities;

        /// <summary>
        /// Производиться ли обработка контроллера в Update.
        /// </summary>
        protected abstract bool IsProcessInUpdate { get; }

        private void Awake()
        {
            controlledEntities = new List<IMoveable>();
            Initialize();
        }

        private void FixedUpdate()
        {
            if (!IsProcessInUpdate) ProcessControlledEntities();
        }
        private void Update()
        {
            if (IsProcessInUpdate) ProcessControlledEntities();
        }

        public virtual void Initialize()
        {

        }

        /// <summary>
        /// Запрос на изменение состояния сущности.
        /// </summary>
        /// <param name="entity">Сущность, для которой происходит изменение.</param>
        /// <param name="requestData">Данные запроса.</param>
        public void RequestStateChange(IMoveable entity, AMovementStateChageRequestData requestData)
        {
            if (entity is null) return;

            if (entity.MovementController != this)
            {
                entity.MovementController.RemoveControllerFromEntity(entity);
                SetEntityToControl(entity);
            }

            ProcessChangeState(entity, requestData);
        }

        /// <summary>
        /// Обработать запрос на изменение состояния сущности.
        /// </summary>
        /// <param name="entity">Сущность, для которой происходит изменение.</param>
        /// <param name="requestData">Данные запроса.</param>
        protected abstract void ProcessChangeState(IMoveable entity, AMovementStateChageRequestData requestData);

        /// <summary>
        /// Передать сущность под контроль.
        /// Установить первоначальное состояние для <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">Объект, способный к передвижению.</param>
        public void SetEntityToControl(IMoveable entity)
        {
            if (entity is null) return;

            entity.MovementController = this;
            entity.MovementState = DefaultState;
            controlledEntities.Add(entity);
        }

        /// <summary>
        /// Удалить контроллер передвижения с сущности.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        public void RemoveControllerFromEntity(IMoveable entity)
        {
            if (entity is null) return;

            controlledEntities.Remove(entity);
        }

        /// <summary>
        /// Передвигается ли сущность.
        /// </summary>
        /// <param name="entity">Сущность для проверки.</param>
        /// <returns>
        /// Возвращает <see langword="true"/>, если считается, что сущность передвигается; 
        /// в противном случае возвращает <see langword="false"/>.
        /// </returns>
        public bool IsMoving(IMoveable entity)
        {
            if (entity?.MovementState is null)
                return false;

            return entity.MovementState.IsDescriptableBy(MovementStateDescriptor.Moving);
        }

        /// <summary>
        /// Обработать передвижение всех сущностей, подвластных контроллеру передвижения.
        /// </summary>
        private void ProcessControlledEntities()
        {
            for (int i = 0; i < controlledEntities.Count; i++)
                ProcessEntity(controlledEntities[i]);
        }

        /// <summary>
        /// Обработать передвижение сущности за текущий кадр.
        /// </summary>
        /// <param name="entity">Сущность.</param>
        private void ProcessEntity(IMoveable entity)
        {
            entity.MovementState.Process(entity);
        }
    }
}
