using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerName : MonoBehaviour
{
    Player player;
    public TextMeshProUGUI jobString;
    public TextMeshProUGUI nameString;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    void Start()
    {
        nameString.text = player.playerName;
    }
}
