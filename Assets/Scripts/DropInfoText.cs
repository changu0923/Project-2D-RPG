using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropInfoText : MonoBehaviour
{
    TextMeshProUGUI dropInfoText;
    bool isUpdated;
    private void Awake()
    {
        dropInfoText = GetComponent<TextMeshProUGUI>();
    }

    void UpdateText(string newText)
    {
        isUpdated = true;
        dropInfoText.alpha = 1;
        dropInfoText.text = dropInfoText.text + newText;
        StartCoroutine(InfoTextReset());
    }
    public void UpdateExpText(int exp)
    {
        string infoString = $"����ġ�� ������ϴ�.(+{exp})\n";
        UpdateText(infoString);
    }

    public void UpdateMoneyText(int money)
    {
        string infoString = $"�޼Ҹ� ������ϴ�.(+{money})\n";
        UpdateText(infoString);
    }

    //TODO: itemȹ��
    //public void UpdateItemText(Item item)
    //{
    //    string infoString = $"�������� ������ϴ�.(+{item.name})\n";
    //    UpdateText(infoString);
    //}

    IEnumerator InfoTextReset()
    {        
        dropInfoText.alpha = 1.0f;
        float fadeDuration = 5f;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            dropInfoText.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        dropInfoText.alpha = 0f;              
    }
}
