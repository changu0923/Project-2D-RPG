using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("BGM")]
    public AudioClip titleBGM;
    public AudioClip battleBGM;

    [Header("Monsters")]
    public AudioClip slimeDamage;
    public AudioClip slimeDie;

    [Header("Player")]
    public AudioClip magicClawUse;
    public AudioClip fireArrowUse;
    public AudioClip meteorUse;
    public AudioClip meteorHit;
    public AudioClip playerDie;
    public AudioClip playerLevelUp;
    public AudioClip playerJump;
    public AudioClip playerUsePortal;

    [Header("Items")]
    public AudioClip itemPick;
    public AudioClip itemPotionDrink;

    [Header("UI")]
    public AudioClip UIButtonClick;

    AudioSource audioSourceBGM;

    private void Awake()
    {
        audioSourceBGM = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        audioSourceBGM.loop = true;
        audioSourceBGM.clip = titleBGM;
        audioSourceBGM.Play();
    }

    public void PlayBGM(AudioClip clip)
    {
        StartCoroutine(FadeOutBGM(clip));
    }

    private IEnumerator FadeOutBGM(AudioClip newBGM)
    {
        float fadeDuration = 1.0f;
        float startVolume = audioSourceBGM.volume;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            audioSourceBGM.volume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        audioSourceBGM.clip = newBGM;
        audioSourceBGM.Play();
        StartCoroutine(FadeInBGM());
    }

    private IEnumerator FadeInBGM()
    {
        float fadeDuration = 1.0f;
        float targetVolume = 1.0f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            audioSourceBGM.volume = Mathf.Lerp(0f, targetVolume, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
