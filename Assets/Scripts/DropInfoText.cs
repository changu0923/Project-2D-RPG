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
        string expString = $"경험치를 얻었습니다.(+{exp})\n";
        UpdateText(expString);
    }

    public void UpdateMoneyText(int money)
    {
        string moneyString = $"메소를 얻었습니다.(+{money})\n";
        UpdateText(moneyString);
    }

    //TODO: item획득
    //public void UpdateItemText(Item item)
    //{
    //    string itemString = $"아이템을 얻었습니다.(+{item.name})\n";
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
