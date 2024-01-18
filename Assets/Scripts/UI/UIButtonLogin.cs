using System.Collections;
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
        GameManager.Instance.sceneLoader.LoadScene("BattleScene");
        GameManager.Instance.soundManager.PlayBGM(GameManager.Instance.soundManager.battleBGM);
    } 
}
