using UnityEngine;

namespace Airashe.Core.Common.Utils
{
    public sealed class SmoothUtility
    {
        /// <summary>
        /// Интерполировать число, к целевому значению.
        /// </summary>
        /// <param name="current">Текущее значение.</param>
        /// <param name="target">Целевое значение.</param>
        /// <param name="threshold">Трешхолд проверки достижения значения.</param>
        /// <param name="interpolationSpeed">Скорость интерполяции.</param>
        /// <returns>
        /// Возвращает значение интерполированние за время прошедшее с прошлого кадра.
        /// </returns>
        public static float Interpolate(float current, float target, float threshold = 0.1f, float interpolationSpeed = 1.0f)
        {
            if (Mathf.Abs(current - target) <= threshold)
                return current;

            return Mathf.Lerp(current, target, interpolationSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Интерполировать позицию, к целевому значению.
        /// </summary>
        /// <param name="current">Текущее значение.</param>
        /// <param name="target">Целевое значение.</param>
        /// <param name="threshold">Трешхолд проверки достижения значения.</param>
        /// <param name="interpolationSpeed">Скорость интерполяции.</param>
        /// <returns>
        /// Возвращает значение интерполированние за время прошедшее с прошлого кадра.
        /// </returns>
        public static Vector2 Interpolate(Vector2 current, Vector2 target, float threshold = 0.1f, float interpolationSpeed = 1.0f)
        {
            if (Vector2.Distance(current, target) <= threshold)
                return current;

            return Vector2.Lerp(current, target, interpolationSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Интерполировать позицию, к целевому значению.
        /// </summary>
        /// <param name="current">Текущее значение.</param>
        /// <param name="target">Целевое значение.</param>
        /// <param name="threshold">Трешхолд проверки достижения значения.</param>
        /// <param name="interpolationSpeed">Скорость интерполяции.</param>
        /// <returns>
        /// Возвращает значение интерполированние за время прошедшее с прошлого кадра.
        /// </returns>
        public static Vector3 Interpolate(Vector3 current, Vector3 target, float threshold = 0.1f, float interpolationSpeed = 1.0f)
        {
            if (Vector3.Distance(current, target) <= threshold)
                return current;

            return Vector3.MoveTowards(current, target, interpolationSpeed * Time.deltaTime);
        }
    }
}
