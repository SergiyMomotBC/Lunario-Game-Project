using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    private static object threadLock = new object();
    private static bool applicationIsQuitting;

    public static T Instance
    {
        get 
        {
            if (applicationIsQuitting)
                return null;

            lock (threadLock)
            {
                if (instance == null)
                {
                    instance = (T) FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        var singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = "[Singleton] " + typeof(T).ToString();
                        DontDestroyOnLoad(singleton);
                    }
                }

                return instance;
            }
        }
    }
    
    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}