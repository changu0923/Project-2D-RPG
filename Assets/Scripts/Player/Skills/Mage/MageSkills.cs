using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class MageSkills : Skill
{
    public GameObject fireArrowPrefab;
    public GameObject fireArrowBasePrefab;
    public GameObject MeteorSkillPrefab;
    public GameObject MagicClawPrefab;
    public Transform shotPoint;
    public Transform bodyEffectPoint;

    bool isCoolTime;
    bool isTeleport;
    Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public override void Use(string skillName)
    {
        Invoke($"{skillName}", 0f);
    }

    IEnumerator CoolTimeWaitingCoroutine(float startTime, float coolTime, float endTime)
    {    
        Player player = GetComponentInParent<Player>();
        player.isMoveAble = false;
        yield return new WaitForSeconds(startTime);
        player.isMoveAble = true;
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
        GameObject magicClaw = Instantiate(MagicClawPrefab, gameObject.transform.position, Quaternion.identity);
        magicClaw.transform.parent = gameObject.transform;
        isCoolTime = true;
        StartCoroutine(CoolTimeWaitingCoroutine(.1f, .33f, 0f)); 
    }

    private void MeteorShower()
    {
        if (isCoolTime == false)
        {
            GameObject meteor = Instantiate(MeteorSkillPrefab, gameObject.transform.position, Quaternion.identity);
            meteor.transform.parent = gameObject.transform;
            isCoolTime = true;
            StartCoroutine(CoolTimeWaitingCoroutine(1.5f, 3f, 0f));
        }
        else
        {
            print($"{Time.deltaTime} : 메테오 쿨타임 진행중");
        }
    }
}
