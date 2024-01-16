using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryPannel : MonoBehaviour
{
    public GameObject content;
    Player player;
    private void Awake()
    {
        player = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            content.SetActive(!content.activeSelf);
            if(content.activeSelf == true)
            {
                TextMeshProUGUI text = content.GetComponentInChildren<TextMeshProUGUI>();
                text.text = player.money.ToString();
            }
        }
    }
}
