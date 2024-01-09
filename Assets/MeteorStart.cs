using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorStart : MonoBehaviour
{
    public GameObject fireballPrefab;
    public GameObject parentGameobject;
    Animator animator;
    void StartFall()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            Transform targetParent = enemy.transform;
            Transform target = enemy.GetComponent<Transform>().Find("FootPoint");
           

            if(target != null) 
            {
                GameObject fireball =  Instantiate(fireballPrefab, target.position, Quaternion.identity);
                fireball.transform.parent = targetParent;
            }
        }
    }
    
    void EndAnimation()
    {        
        Destroy(parentGameobject);
    }
}
