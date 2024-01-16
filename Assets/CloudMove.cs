using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public float moveSpeed = 0.3f;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(Time.deltaTime * moveSpeed, 0f);
        spriteRenderer.material.mainTextureOffset = offset;
    }
}
