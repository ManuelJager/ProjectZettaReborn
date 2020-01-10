using UnityEngine;

namespace Zetta.Generics
{
    public abstract class AutoInstanceMonoBehaviour<T> : MonoBehaviour
        where T : AutoInstanceMonoBehaviour<T>
    {
        public static bool destroyed { get; private set; }

        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance)
                {
                    return instance;
                }

                instance = GetAutoMonoBehaviour(dontDestroyOnLoad: true);
                destroyed = false;
                return instance;
            }
        }

        protected void Awake()
        {
            DontDestroyOnLoad(this);
            if (!instance || destroyed)
            {
                instance = (T)this;
                instance.gameObject.name += " (singleton)";
                destroyed = false;
            }
        }

        protected void OnDestroy()
        {
            if (ReferenceEquals(this, instance))
            {
                destroyed = true;
            }
        }

        public static void Echo()
        {
            instance = GetAutoMonoBehaviour(true);
        }

        public static T GetAutoMonoBehaviour(bool dontDestroyOnLoad = false)
        {
            var result = FindObjectOfType<T>();
            if (!result)
            {
                result = new GameObject(
                    $"Auto instance: {typeof(T).Name}"
                ).AddComponent<T>();
            }

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(result.gameObject);
            }

            return result;
        }
    }
}