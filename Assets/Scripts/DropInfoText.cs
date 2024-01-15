using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropInfoText : MonoBehaviour
{
    TextMeshProUGUI dropInfoText;
    Coroutine InfoTextResetCoroutine;
    private void Awake()
    {
        dropInfoText = GetComponent<TextMeshProUGUI>();
        dropInfoText.alpha = 1.0f;
    }

    void UpdateText(string newText)
    {
        dropInfoText.text = dropInfoText.text + newText;
        if (InfoTextResetCoroutine != null)
        {
            StopCoroutine(InfoTextResetCoroutine);
        }
        InfoTextResetCoroutine = StartCoroutine(InfoTextReset());
    }

    public void UpdateExpText(int exp)
    {
        string expString = $"����ġ�� ������ϴ�.(+{exp})\n";
        UpdateText(expString);
    }

    public void UpdateMoneyText(int money)
    {
        string moneyString = $"�޼Ҹ� ������ϴ�.(+{money})\n";
        UpdateText(moneyString);
    }

    //TODO: itemȹ��
    //public void UpdateItemText(Item item)
    //{
    //    string itemString = $"�������� ������ϴ�.(+{item.name})\n";
    //    UpdateText(itemString);
    //}

    IEnumerator InfoTextReset()
    {
        dropInfoText.alpha = 1.0f;

        yield return new WaitForSeconds(2f);

        float fadeDuration = 1f;
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
