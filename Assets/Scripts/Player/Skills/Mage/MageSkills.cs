using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using static Player;

public class MageSkills : Skill
{
    public GameObject fireArrowPrefab;
    public GameObject MeteorSkillPrefab;
    public GameObject MagicClawPrefab;
    public Transform shotPoint;
    public Transform bodyEffectPoint;

    bool isCoolTime;
    Player player;
    private void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    public override void Use(string skillName)
    {
        Invoke($"{skillName}", 0f);
    }

    private void FireArrow()
    {
        if (player.level >= 5 && player.currentMP >= 15 && isCoolTime == false)
        {          
            player.currentMP -= 15;
            GameObject arrow = Instantiate(fireArrowPrefab);
            arrow.transform.position = shotPoint.position;
            arrow.transform.rotation = shotPoint.rotation;
            Destroy(arrow, 0.5f);
            isCoolTime = true;
            StartCoroutine(CoolTimeWaitingCoroutine(.75f, 1f, 0f));
        }
    } 

    private void MagicClaw()
    {
        if (player.currentMP >= 5 && isCoolTime == false)
        {
            player.SetAttackMotion(AttackMotion.SWING);
            player.SetState(State.ATTACK);
            player.currentMP -= 5;
            GameObject magicClaw = Instantiate(MagicClawPrefab, gameObject.transform.position, Quaternion.identity);
            magicClaw.transform.parent = gameObject.transform;
            isCoolTime = true;
            StartCoroutine(CoolTimeWaitingCoroutine(1f, .74f, 0f));
        }
    }

    private void MeteorShower()
    {
        if (player.level >= 10 && player.currentMP >= 30 && isCoolTime == false)
        {
            player.SetAttackMotion(AttackMotion.NORMAL);
            player.SetState(State.ATTACK);
            player.currentMP -= 30;
            GameObject meteor = Instantiate(MeteorSkillPrefab, gameObject.transform.position, Quaternion.identity);
            meteor.transform.parent = gameObject.transform;
            isCoolTime = true;
            StartCoroutine(CoolTimeWaitingCoroutine(1.5f, 3f, 0f));
        }
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
}
