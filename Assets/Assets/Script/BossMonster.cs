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
        Debug.Log($"���� ������ ü�� : {BossMonsterNowHp}");
    }


    private void Update()
    {

        Player player = GameObject.Find("Player").GetComponent<Player>();
        Slider PlayerHp = GameObject.Find("PlayerHPBar").GetComponent<Slider>();

        if (player.transform.position.x > transform.position.x - 2)
        {
            anim.SetBool("BossBaseAttack", true);
            //Debug.Log("������ �����ߴ�");

            if (PlayerHp.value <= 0)
            {
                anim.SetBool("BossBaseAttack", false);
                //Debug.Log("�÷��̾ ����ؼ� ������ ������ �����");
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


            Debug.Log($"���Ͱ� {Player.PlayerAP} �� Ÿ���� ����");
            Debug.Log($"���� HP : {BossMonsterNowHp}");

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
