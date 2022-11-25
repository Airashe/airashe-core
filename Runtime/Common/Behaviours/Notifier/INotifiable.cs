namespace Airashe.Core.Common.Behaviours
{
    /// <summary>
    /// Интерфейс объекта, ожидающего уведомления о событии.
    /// </summary>
    /// <typeparam name="T">Тип данных события.</typeparam>
    public interface INotifiable<T>
    {
        /// <summary>
        /// Уведомить экземпляр о событии.
        /// </summary>
        /// <param name="eventData">Данные события.</param>
        public void Notify(T eventData);
    }
}
