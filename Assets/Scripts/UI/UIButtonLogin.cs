using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonLogin : MonoBehaviour
{
    Button loginButton;
    InputField playerNameInputField;

    private void Awake()
    {
        loginButton = GetComponent<Button>();
        playerNameInputField = FindObjectOfType<InputField>();
    }

    public void OnButtonClicked()
    {
        GameManager.Instance.getString = playerNameInputField.text;
        GameManager.Instance.sceneLoader.LoadScene("BattleScene");
    }

}
