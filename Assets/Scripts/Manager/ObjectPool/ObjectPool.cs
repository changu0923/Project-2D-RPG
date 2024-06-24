using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    private Queue<T> pool = new Queue<T>();
    private T prefab;
    private Transform parent;
    private int capacity;

    public ObjectPool(T prefab, int capacity = 10, Transform parent = null)
    {
        this.prefab = prefab;
        this.parent = parent;
        this.capacity = capacity;

        for (int i = 0; i < capacity; i++)
        {
            T newObj = Object.Instantiate(prefab, parent);
            newObj.gameObject.SetActive(false);
            pool.Enqueue(newObj);
        }
    }

    public T Get()
    {
        if (pool.Count > 0)
        {
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            AddCapacity(capacity);
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }
    }

    private void AddCapacity(int count)
    {
        for(int i = 0; i < count;)
        {
            T new_obj = Object.Instantiate(prefab, parent); 
            new_obj.gameObject.SetActive(false);
            pool.Enqueue(new_obj);
        }
    }


    public void Return(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Enqueue(obj);
    }
}