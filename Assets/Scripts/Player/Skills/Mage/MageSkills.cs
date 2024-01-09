using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MageSkills : Skill
{
    public GameObject fireArrowPrefab;
    public GameObject fireArrowBasePrefab;
    public GameObject MeteorHitPrefab;
    public Transform shotPoint;
    public Transform bodyEffectPoint;

    bool isCoolTime;
    bool isTeleport;

    public override void Use(string skillName)
    {
        Invoke($"{skillName}", 0f);
    }

    IEnumerator CoolTimeWaitingCoroutine(float startTime, float coolTime, float endTime)
    {    
        yield return new WaitForSeconds(coolTime);
        isCoolTime = false;
    }
    IEnumerator CoolTimeTeleportCoroutine(float coolTime)
    {
        yield return new WaitForSeconds(coolTime);
        isCoolTime = false;
    }

    private void FireArrow()
    {
        if (isCoolTime==false)
        {    
            GameObject arrow = Instantiate(fireArrowPrefab);
            GameObject effect = Instantiate(fireArrowBasePrefab);
            effect.transform.position = shotPoint.position;
            effect.transform.rotation = shotPoint.rotation;
            arrow.transform.position = shotPoint.position;
            arrow.transform.rotation = shotPoint.rotation;
            Destroy(arrow, 0.5f); Destroy(effect, 0.8f);

            isCoolTime = true;            
            StartCoroutine(CoolTimeWaitingCoroutine(0f, 1f, 0f));
        }       
    } 

    private void Teleport()
    {
        if (isTeleport == false)
        {          
            // 여기에 텔레포트 스크립트 작성


            isTeleport = true;
            StartCoroutine("CoolTimeTeleportCoroutine", .36f);
        }        
    }
    private void MagicClaw()
    {
       
    }

    private void MeteorShower()
    {
        if (isCoolTime == false)
        {
            List<GameObject> enemyList = new List<GameObject>();    

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemyList.AddRange(enemies);

            print("메테오 플레이어 이펙트 발동");
            if (enemyList.Count > 0)
            {
                foreach (GameObject enemy in enemyList)
                {                    
                    Enemy target = enemy.GetComponent<Enemy>();
                    float damage = 30000f;
                    int minDamage = (int)(damage - (damage * 0.15));
                    int maxDamage = (int)(damage + (damage * 0.15));

                    int rndDamage = Random.Range(minDamage, maxDamage);
                    target.TakeDamage(rndDamage);
                }
            }
            isCoolTime = true;
            StartCoroutine(CoolTimeWaitingCoroutine(0f, 3f, 0f));
        }
        else
        {
            print($"{Time.deltaTime} : 메테오 쿨타임 진행중");
        }
    }
}
