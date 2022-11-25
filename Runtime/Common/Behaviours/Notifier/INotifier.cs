namespace Airashe.Core.Common.Behaviours
{
    /// <summary>
    /// Интерфейс объекта, уведомляющего подписчиков о событии.
    /// </summary>
    /// <typeparam name="T">Тип, содержащий информацию об уведомлении.</typeparam>
    public interface INotifier<T>
    {
        /// <summary>
        /// Подписать экземпляр <paramref name="instance"/> на уведомления.
        /// </summary>
        /// <param name="instance">Подписываемый экземпляр.</param>
        public void Subscribe(INotifiable<T> instance);

        /// <summary>
        /// Отписать экземпляр <paramref name="instaince"/> от уведомлений.
        /// </summary>
        /// <param name="instaince">Отписывающийся экземпляр.</param>
        public void Unsubscribe(INotifiable<T> instaince);
    }
}
