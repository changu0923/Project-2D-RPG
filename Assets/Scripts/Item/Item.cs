using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    string itemName;
    string itemDesc;
    int itemValue;

    public abstract void Use();
}
