using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float posValue;

    Vector2 startPos;
    float newPos;

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newPos = Mathf.Repeat(Time.time * moveSpeed, posValue);
        transform.position = startPos + Vector2.right * newPos;
    }
}
