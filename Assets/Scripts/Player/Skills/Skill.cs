using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public int skillLevel;
    public AudioSource skillAudio;
    public abstract void Use(string skillName);
}
