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
    public float barAnimationSpeed = 2f;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        showHPBar();
    }

    private void Update()
    {
        showHPBar();
    }
    public void showHPBar()
    {     
        float updatedHP  = 1f - (float)player.currentHP / player.maxHP;
        float updatedMP  = 1f - (float)player.currentMP / player.maxMP;
        float updatedExp = 1f - (float)player.currentEXP / player.maxEXP;

        currentHPBar.fillAmount = Mathf.Lerp(currentHPBar.fillAmount, updatedHP, barAnimationSpeed * Time.deltaTime);
        currentMPBar.fillAmount = Mathf.Lerp(currentMPBar.fillAmount, updatedMP, barAnimationSpeed * Time.deltaTime);
        currentExpBar.fillAmount = Mathf.Lerp(currentExpBar.fillAmount, updatedExp, barAnimationSpeed * Time.deltaTime);

        HPText.text = $"[{player.currentHP}/{player.maxHP}]";
        MPText.text = $"[{player.currentMP}/{player.maxMP}]";
        EXPText.text = $"[{player.currentEXP}/{player.maxEXP}]";
        //TODO: Name, Level
    }
}
