using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T instance;

    protected virtual bool DontDestroy => false;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<T>();

                if (instance == null)
                {
                    Debug.LogError($"{typeof(T).Name} ‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ");
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (!CheckInstance())
            return;

        if (DontDestroy)
            DontDestroyOnLoad(gameObject);
    }

    private bool CheckInstance()
    {
        if (instance == null)
        {
            instance = this as T;
            return true;
        }

        if (instance == this)
            return true;

        Destroy(gameObject);
        return false;
    }
}