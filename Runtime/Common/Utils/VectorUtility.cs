using UnityEngine;

namespace Airashe.Core.Common.Utils
{
    /// <summary>
    /// Утилитарный класс для работы с векторами.
    /// </summary>
    public sealed class VectorUtility
    {
        /// <summary>
        /// Возвращает положение <paramref name="targetDirection"/> относительно <paramref name="forward"/>.
        /// </summary>
        /// <param name="forward">Направление вперед.</param>
        /// <param name="targetDirection">Позиция объекта.</param>
        /// <returns>
        /// <para>Возвращает 1, если <paramref name="targetDirection"/> слева относительно <paramref name="forward"/>;</para>
        /// <para>Возвращает -1, если <paramref name="targetDirection"/> справа относительно <paramref name="forward"/>;</para>
        /// <para>Возвращает 0, если <paramref name="targetDirection"/> спереди или сзади относительно <paramref name="forward"/>;</para>
        /// </returns>
        public static int AngleDir(Vector3 forward, Vector3 targetDirection)
        {
            var prep = Vector3.Cross(forward, targetDirection);
            float direction = Vector3.Dot(prep, Vector3.up);

            if (direction > 0.0f)
                return 1;
            else if (direction < 0.0f)
                return -1;
            else
                return 0;
        }

        /// <summary>
        /// Возвращает положение <paramref name="targetDirection"/> относительно <paramref name="forward"/>.
        /// </summary>
        /// <param name="forward">Направление вперед.</param>
        /// <param name="targetDirection">Позиция объекта.</param>
        /// <returns>
        /// <para>Возвращает 1, если <paramref name="targetDirection"/> слева относительно <paramref name="forward"/>;</para>
        /// <para>Возвращает -1, если <paramref name="targetDirection"/> справа относительно <paramref name="forward"/>;</para>
        /// <para>Возвращает 0, если <paramref name="targetDirection"/> спереди или сзади относительно <paramref name="forward"/>;</para>
        /// </returns>
        public static int AngleDir(Vector2 forward, Vector2 targetDirection)
        {
            float direction = -forward.x * targetDirection.y + forward.y * targetDirection.x;
            if (direction > 0.0f)
                return -1;
            else if (direction < 0.0f)
                return 1;
            else
                return 0;
        }
    }
}
