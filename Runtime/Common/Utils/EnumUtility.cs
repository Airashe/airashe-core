using System;

namespace Airashe.Core.Common.Utils
{
    /// <summary>
    /// Утилитарный класс для работы с <see cref="Enum"/>.
    /// </summary>
    public sealed class EnumUtility
    {
        /// <summary>
        /// Получить все возможные значения <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Тип перечисления.</typeparam>
        /// <returns>
        /// Возвращает массив всех возможных значений <typeparamref name="T"/>.
        /// </returns>
        public static T[] GetValues<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }
    }
}
