using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance; // only want one of this object
    public static T Instance
    {
        get
        {
            // if instance hasn't been set yet, set it.
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        // if instance hasn't been set yet, set it.
        if (instance == null)
        {
            instance = this as T;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}