using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpEffect : MonoBehaviour
{
    public GameObject thisGameObject;
    // Start is called before the first frame update
    

    void OnAnimationEnd()
    {
        Destroy(thisGameObject);
    }
}
