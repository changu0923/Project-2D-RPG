using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    public int skillLevel;
    public abstract void Use(string skillName);
}
