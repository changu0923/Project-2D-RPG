using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class DamagePopup : MonoBehaviour
{
    public GameObject[] numberPrefabs;
    public Transform target;
    float spacing = .23f;
    float lifeTime = 1f;
    float flyingSpeed = .5f;

    public void PrintDamage(int damage)
    {       
        string damageText = damage.ToString();

        GameObject damageFontParent = new GameObject("DamageFontParent");

        for (int i = 0; i < damageText.Length; i++)
        {
            float ySize = 0.02f;
            int digit = int.Parse(damageText[i].ToString());

            if (i % 2 == 0)
            {
                ySize = +ySize;
            }
            else
            {
                ySize = -ySize;            
            }

            Vector3 calculatedCenter = new Vector3((spacing * (damageText.Length-1))/2, 0, 0);

            GameObject numberObject = Instantiate(numberPrefabs[digit], target.position - calculatedCenter + new Vector3(i * spacing, ySize, 0f), Quaternion.identity);
            if(i==0)
            {
                numberObject.transform.localScale = new Vector3(1.2f,1.2f,1.2f);
            }
            numberObject.GetComponent<SpriteRenderer>().sortingOrder = 1000 + i;
            numberObject.transform.parent = damageFontParent.transform;
            StartCoroutine("Fly", numberObject);
        }
        Destroy(damageFontParent, 1.6f);
    }

    private IEnumerator Fly(GameObject number)
    {
        SpriteRenderer spriteRenderer = number.GetComponent<SpriteRenderer>();

        float startAlpha = spriteRenderer.color.a;
        float currentTime = 0f;

        while (currentTime < lifeTime)
        {
            float alpha = Mathf.Lerp(startAlpha, 0f, currentTime / lifeTime);
            Color newColor = spriteRenderer.color;
            newColor.a = alpha;
            spriteRenderer.color = newColor;

            number.transform.Translate(Vector2.up * flyingSpeed * Time.deltaTime, Space.World);
            currentTime += Time.deltaTime;
            yield return null;
        }
        Destroy(number.gameObject);
    }   
}
