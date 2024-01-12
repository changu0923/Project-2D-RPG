using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Player player;

    public Image currentHPBar;
    public Image currentMPBar;
    public Image currentExpBar;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI MPText;
    public TextMeshProUGUI EXPText;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        showHPBar();
    }

    public void showHPBar()
    {
        currentHPBar.fillAmount  = 1f - (float)player.currentHP / player.maxHP;
        currentMPBar.fillAmount  = 1f - (float)player.currentMP / player.maxMP;
        currentExpBar.fillAmount = 1f - (float)player.currentEXP / player.maxEXP;
        HPText.text = $"[{player.currentHP}/{player.maxHP}]";
        MPText.text = $"[{player.currentMP}/{player.maxMP}]";
        EXPText.text = $"[{player.currentEXP}/{player.maxEXP}]";
        //TODO: Name, Level
    }

}
