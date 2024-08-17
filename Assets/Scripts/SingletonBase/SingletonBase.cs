using UnityEngine;

namespace SingletonBase
{
    namespace DontDestroySingleton
    {
        public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
        {
            private static T instance;
            public static T Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<T>();
                        if (instance == null)
                        {
                            Debug.LogError($"_instance null");
                        }
                    }

                    return instance;
                }
            }

            private void Awake()
            {
                if (instance == null)
                {
                    instance = this as T;
                }
                else if (instance != this)
                {
                    Destroy(gameObject);
                }

                DoAwake();
                DontDestroyOnLoad(gameObject);
            }

            protected virtual void DoAwake()
            {

            }
        }
    }

    namespace DestroySingleton
    {
        public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
        {
            private static T instance;
            public static T Instance
            {
                get
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<T>();
                        if (instance == null)
                        {
                            Debug.LogError($"_instance null");
                        }
                    }

                    return instance;
                }
            }

            private void Awake()
            {
                if (instance == null)
                {
                    instance = this as T;
                }
                else if (instance != this)
                {
                    Destroy(gameObject);
                }

                DoAwake();
            }

            protected virtual void DoAwake()
            {

            }
        }
    }
}
