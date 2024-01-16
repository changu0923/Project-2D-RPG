using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Player player;
    public SceneLoader sceneLoader;

    public string getString;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        sceneLoader = GetComponent<SceneLoader>();
    }   
}