using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    string itemName;
    string itemDesc;
    int itemValue;

    [Header("아이템 유통기한")]
    public float existTime = 30f;

    public abstract void Use();

    private void Start()
    {
        StartCoroutine(ExpirationTime());
    }

    IEnumerator ExpirationTime()
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
