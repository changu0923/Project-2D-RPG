using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    #region Signleton
    private static ObjectPoolManager instance = null;
    public static ObjectPoolManager Instance
    {
        get
        {
            if(instance == null)
            {
                var obj = FindObjectOfType<ObjectPoolManager>();
                if(obj != null) 
                {
                    instance = obj;
                }
                else
                {
                    var new_obj = new GameObject();
                    instance = new_obj.AddComponent<ObjectPoolManager>();
                    new_obj.name = instance.GetType().Name;
                }
            }
            return instance;
        }
    }
    #endregion

    private Dictionary<string, object> pools = new Dictionary<string, object>();

    public void CreatePool<T> (T prefab, int initialSize ) where T : MonoBehaviour
    {
        var pool = new ObjectPool<T>(prefab, initialSize, transform);
    }

    public T Get<T>() where T : MonoBehaviour
    {
        if (pools.TryGetValue(typeof(T).Name, out var pool))
        {
            return ((ObjectPool<T>)pool).Get();
        }

        Debug.LogError($"Pool for type {typeof(T).Name} not found.");
        return null;
    }

    public void Return<T>(T obj) where T : MonoBehaviour
    {
        if (pools.TryGetValue(typeof(T).Name, out var pool))
        {
            ((ObjectPool<T>)pool).Return(obj);
        }
        else
        {
            Debug.LogError($"Pool for type {typeof(T).Name} not found.");
        }
    }
}