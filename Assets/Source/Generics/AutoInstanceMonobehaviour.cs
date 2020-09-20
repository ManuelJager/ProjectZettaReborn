using UnityEngine;

namespace Zetta.Generics
{
    public abstract class AutoInstanceMonoBehaviour<T> : MonoBehaviour
        where T : AutoInstanceMonoBehaviour<T>
    {
        public static bool destroyed { get; private set; }

        public static bool dontDestroyOnLoad = false;

        protected static T instance;

        public static T Instance
        {
            get
            {
                if (instance)
                {
                    return instance;
                }

                instance = GetAutoMonoBehaviour(dontDestroyOnLoad: dontDestroyOnLoad);
                destroyed = false;
                return instance;
            }
        }

        protected void Awake()
        {
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

            if (dontDestroyOnLoad)
            {
                DontDestroyOnLoad(result.gameObject);
            }

            return result;
        }
    }
}