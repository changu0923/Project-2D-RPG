using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonLogin : MonoBehaviour
{
    Button loginButton;
    InputField playerNameInputField;
    AudioSource btClickSound;
    private void Awake()
    {
        loginButton = GetComponent<Button>();
        playerNameInputField = FindObjectOfType<InputField>();
        btClickSound = gameObject.AddComponent<AudioSource>();
        btClickSound.loop = false;
        btClickSound.clip = GameManager.Instance.soundManager.UIButtonClick;
    }

    public void OnButtonClicked()
    {
        btClickSound.PlayOneShot(btClickSound.clip);
        GameManager.Instance.getString = playerNameInputField.text;
        GameManager.Instance.StopBGM();
        GameManager.Instance.PlayBGM(GameManager.Instance.soundManager.battleBGM);
        GameManager.Instance.sceneLoader.LoadScene("BattleScene");
    }

}
