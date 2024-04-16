using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossMonster : MonoBehaviour
{
    private int BossMonsterNowHp = GameManager.BossMonsterNowHP;
    private int BossMonsterAP = GameManager.BossMonsterAP;
    private int playerAP = GameManager.PlayerAP;

    public Transform target;

    public float attackDelay = 3f;
    
    Animator anim;
   
    private void Awake()
    {
        anim = GetComponent<Animator>();
       
    }
    private void Start()
    {
        Debug.Log($"현재 보스몹 체력 : {BossMonsterNowHp}");
    }


    private void Update()
    {

        Player player = GameObject.Find("Player").GetComponent<Player>();
        Slider PlayerHp = GameObject.Find("PlayerHPBar").GetComponent<Slider>();

        if (player.transform.position.x > transform.position.x - 2)
        {
            anim.SetBool("BossBaseAttack", true);
            //Debug.Log("보스가 공격했다");

            if (PlayerHp.value <= 0)
            {
                anim.SetBool("BossBaseAttack", false);
                //Debug.Log("플레이어가 사망해서 보스가 공격을 멈췄다");
            }
        }
        else
        {
            anim.SetBool("BossBaseAttack", false);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player Player = GameObject.Find("Player").GetComponent<Player>();

        if (other.CompareTag("HitBox"))
        {
            anim.SetTrigger("MonsterDamaged");
            BossMonsterNowHp -= Player.PlayerAP;


            Debug.Log($"몬스터가 {Player.PlayerAP} 의 타격을 입음");
            Debug.Log($"남은 HP : {BossMonsterNowHp}");

            if (BossMonsterNowHp <= 0)
            {
                anim.SetBool("MonsterDie", true);
                Invoke("MonsterDie", 2.0f);
            }
        }
    }

    void BossIdle()
    {
        anim.SetBool("BossBaseAttack", false);
    }
    void MonsterDie()
    {
        gameObject.SetActive(false);
    }

    void PlayerHitTrue()
    {
        Transform childrenTransform = transform.Find("BossHitBox");
        childrenTransform.gameObject.SetActive(true);
    }
    void PlayerHitFalse()
    {
        Transform childrenTransform = transform.Find("BossHitBox");
        childrenTransform.gameObject.SetActive(false);
    }
}
