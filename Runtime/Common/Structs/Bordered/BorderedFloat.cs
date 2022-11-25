using System;
using UnityEngine;

namespace Airashe.Core.Common.Structs
{
    /// <summary>
    /// Переменная <see cref="float"/> имеющая границы значений.
    /// </summary>
    [Serializable]
    public struct BorderedFloat
    {
        /// <summary>
        /// Текущее значение переменной.
        /// </summary>
        public float Value
        {
            get => currentValue;
            set => SetValidValue(value);
        }

        /// <summary>
        /// Максимальное значение переменной.
        /// </summary>
        public float MaxValue
        {
            get => maxValue;
            set
            {
                maxValue = value;
                SetValidValue(Value);
            }
        }

        /// <summary>
        /// Минимальное значение переменной.
        /// </summary>
        public float MinValue
        {
            get => minValue;
            set
            {
                minValue = value;
                SetValidValue(Value);
            }
        }

        /// <summary>
        /// Проверять ли выход значения за границы.
        /// </summary>
        public bool ApplyBorders => applyBorders;

        /// <summary>
        /// Текущее значение переменной.
        /// </summary>
        [SerializeField] private float currentValue;

        /// <summary>
        /// Максимальное значение переменной.
        /// </summary>
        [SerializeField] private float maxValue;

        /// <summary>
        /// Минимальное значение переменной.
        /// </summary>
        [SerializeField] private float minValue;

        /// <summary>
        /// Проверять ли выход значения за границы.
        /// </summary>
        [SerializeField] private bool applyBorders;

        /// <summary>
        /// Переменная <see cref="float"/> имеющая границы значений.
        /// </summary>
        /// <param name="absoluteValue">Абсолютное значение.</param>
        public BorderedFloat(float absoluteValue)
        {
            currentValue = absoluteValue;
            minValue = float.MinValue;
            maxValue = float.MaxValue;
            applyBorders = true;
        }

        /// <summary>
        /// Установить валидное значение переменной.
        /// </summary>
        /// <param name="value">Желаемое значение переменной.</param>
        private void SetValidValue(float value)
        {
            if (!applyBorders)
            {
                currentValue = value;
                return;
            }

            if (value < minValue)
            {
                currentValue = minValue;
                return;
            }

            if (value > maxValue)
            {
                currentValue = maxValue;
                return;
            }

            currentValue = value;
        }

        public static implicit operator float(BorderedFloat bordered) => bordered.currentValue;
        public static implicit operator BorderedFloat(float absoluteValue) => new BorderedFloat(absoluteValue);
    }
}
