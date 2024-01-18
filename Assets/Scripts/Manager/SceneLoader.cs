using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator animator;
    public float effectTime = 2f;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneFadeEffectCoroutine(sceneName));
    }

    IEnumerator LoadSceneFadeEffectCoroutine(string sceneName)
    {
        animator.SetTrigger("FadeStart");

        yield return new WaitForSeconds(effectTime);        

        SceneManager.LoadScene(sceneName);

        animator.SetTrigger("FadeStart");
    }
}