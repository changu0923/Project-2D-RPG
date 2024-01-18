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
        AudioSource uiAudio = gameObject.AddComponent<AudioSource>();
        uiAudio.PlayOneShot(GameManager.Instance.soundManager.UIButtonClick);
        player.Respawn();
        Destroy(uiAudio);
        parentGameObject.SetActive(false);
    }
}
