using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Player player;
    public SceneLoader sceneLoader;
    public SoundManager soundManager;

    public string getString;
    AudioSource audioSourceBGM;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        soundManager = GetComponent<SoundManager>();
        sceneLoader = GetComponent<SceneLoader>();
        audioSourceBGM = GetComponent<AudioSource>();
        PlayBGM(GameManager.Instance.soundManager.titleBGM);
    }

    public void PlayBGM(AudioClip clip)
    {
        audioSourceBGM.clip = clip;
        audioSourceBGM.Play();
    }

    public void StopBGM()
    {
        audioSourceBGM.Stop();
    }
}