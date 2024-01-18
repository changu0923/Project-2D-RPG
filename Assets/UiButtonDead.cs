using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiButtonDead : MonoBehaviour
{
    Player player;
    public GameObject parentGameObject;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    public void OnRevive()
    {
        print("respawn");
        player.Respawn();
        parentGameObject.SetActive(false);
    }
}
