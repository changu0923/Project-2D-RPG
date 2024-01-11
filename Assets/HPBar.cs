using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    Player player;

    public Image currentHPBar;
    public Image currentMPBar;
    public Image currentExpBar;

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
        currentHPBar.fillAmount = 1f - (float)player.currentHP / player.maxHP;
        currentMPBar.fillAmount = 1f - (float)player.currentMP / player.maxMP;
        currentExpBar.fillAmount = 1f - (float)player.currentEXP / player.maxEXP;
    }

}
