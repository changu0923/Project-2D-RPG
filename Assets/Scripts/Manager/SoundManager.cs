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
}
