using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UILevelNumber : MonoBehaviour
{
    public Transform horizontalLayoutGroup;   
    public Image[] levelNumbers;
    Player player;

    void Start()
    {
        if (GameObject.Find("Player").TryGetComponent(out player))
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        else
        {
            player = null;
        }
        UpdateLevelImage();
    }

    private void Update()
    {
        
    }

    public void UpdateLevelImage()
    {
        string levelToString = player.level.ToString();

        foreach (Transform child in horizontalLayoutGroup)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < levelToString.Length; i++) 
        {
            int digit = int.Parse(levelToString[i].ToString());
            Image numberObject = Instantiate(levelNumbers[digit], horizontalLayoutGroup);
        }
    }

    
}
