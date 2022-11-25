using UnityEngine;

namespace Airashe.Core.Common.Behaviours
{
    /// <summary>
    /// Абстрактный класс, наделяющий <see cref="MonoBehaviour"/> 
    /// поведением синглтона.
    /// </summary>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        /// <summary>
        /// Экземпляр <typeparamref name="T"/>.
        /// </summary>
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance is not null)
            {
                Destroy(this);
                return;
            }

            Instance = (T)this;
            OnAwake();
        }

        /// <summary>
        /// Событие происходящее в <see cref="MonoBehaviour.Awake"/>, 
        /// после инициализации экземпляра синглтона <typeparamref name="T"/>.
        /// </summary>
        protected virtual void OnAwake()
        {

        }
    }
}
