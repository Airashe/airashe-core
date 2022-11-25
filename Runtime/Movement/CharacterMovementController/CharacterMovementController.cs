using UnityEngine;

namespace Airashe.Core.Movement
{
    /// <summary>
    /// Контроллер по умолчанию - для гуманойдных персонажей.
    /// </summary>
    public sealed class CharacterMovementController : AMovementController
    {
        public override AMovementState DefaultState => ACharacterMovementState.Idle;

        protected override bool IsProcessInUpdate => false;

        protected override void ProcessChangeState(IMoveable entity, AMovementStateChageRequestData requestData)
        {
            var data = requestData as ChangeMovementStateByInputCommandRequestData;
            if (data is null) return;

        }
    }
}
