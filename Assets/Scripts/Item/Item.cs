using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    string itemName;
    string itemDesc;
    int itemValue;

    [Header("������ �������")]
    public float existTime = 30f;

    public virtual void Use()
    {

    }

    public IEnumerator ExpirationTime()
    {
        float currentTime = 0f;
        while(currentTime<=existTime)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
