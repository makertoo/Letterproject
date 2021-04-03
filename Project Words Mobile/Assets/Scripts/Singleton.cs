namespace Assets.Scripts
{
    //IMPORT
    using UnityEngine;

    /// <summary>
    /// Makes a script singleton
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// Public Vars
        /// </summary>
        public static T Instance;

        /// <summary>
        /// Protected vars
        /// </summary>
        protected bool destroyOnLoad;

        /// <summary>
        /// Initialization
        /// </summary>
        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = GetComponent<T>();
                if (!destroyOnLoad) DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
