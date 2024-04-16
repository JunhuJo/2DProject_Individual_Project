using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private int MonsternowHp = GameManager.MonsterNowHP;
    private int MonsterAP = GameManager.MonsterAP;
    private int PlayerAP = GameManager.PlayerAP;
   
    MonsterMove monstermove;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        monstermove = GetComponent<MonsterMove>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBox"))
        {
            anim.SetTrigger("MonsterDamaged");
                        
            MonsternowHp -= PlayerAP;

            Debug.Log($"몬스터가 {PlayerAP} 의 타격을 입음");
            Debug.Log($"남은 HP : {MonsternowHp}");
            if (MonsternowHp <= 0)
            {
                monstermove.nextMove = 0;
                anim.SetBool("MonsterDie", true);
                Invoke("MonsterDie", 1.0f);
            }
        }
    }

    void MonsterDie()
    {
        gameObject.SetActive(false);
        
    }

    public void SetMonsterNowHP(int newNowHP)
    {
        MonsternowHp = newNowHP;
    }

    public int GetMonsterNowHP()
    {
        return MonsternowHp;
    }

    public void SetMonsterAP(int newMonsterAP)
    {
        MonsterAP = newMonsterAP;
    }

    public int GetMonsterAP()
    {
        return MonsterAP;
    }

    public void SetPlayerAP(int newPlayerAP)
    {
        PlayerAP = newPlayerAP;
    }

    public int GetPlayerAP()
    {
        return PlayerAP;
    }
}
