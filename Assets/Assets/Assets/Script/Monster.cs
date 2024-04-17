using UnityEngine;

public class Monster : MonoBehaviour
{
    private int MonsternowHp = GameManager.MonsterNowHP;
    private int MonsterAP = GameManager.MonsterAP;
    private int PlayerAP = GameManager.PlayerAP;
    public GameObject[] DropItemPrefab;
    public Vector2 spwanPosition;
    
    public Transform Transform;
    MonsterMove monstermove;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        monstermove = GetComponent<MonsterMove>();
        Transform = GetComponent<Transform>();

        for (int i = 0; i < DropItemPrefab.Length; i++)
        {
            DropItemPrefab[i] = GameObject.FindGameObjectWithTag("Item");
        }
    }

    //���Ͱ� HitBox�� �浹 �� ó��(�����)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBox"))
        {
            anim.SetTrigger("MonsterDamaged");
                        
            MonsternowHp -= PlayerAP;

            Debug.Log($"���Ͱ� {PlayerAP} �� Ÿ���� ����");
            Debug.Log($"���� HP : {MonsternowHp}");
            if (MonsternowHp <= 0)
            {
                monstermove.nextMove = 0;
                anim.SetBool("MonsterDie", true);
                if (anim.GetBool("MonsterDie") == true)
                {
                    int itemNum = Random.Range(0, 3);

                    if (itemNum == 0)
                    {
                        int GetItem = Random.Range(0, 3);
                        if (GetItem == 0)
                        {
                            spwanPosition = transform.position;
                            Instantiate(DropItemPrefab[0], spwanPosition, Quaternion.identity);
                        }
                        else if (GetItem == 1)
                        {
                            spwanPosition = transform.position;
                            spwanPosition = transform.position;
                            Instantiate(DropItemPrefab[1], spwanPosition, Quaternion.identity);
                        }
                        else if (GetItem == 2)
                        {
                            spwanPosition = transform.position;
                            spwanPosition = transform.position;
                            Instantiate(DropItemPrefab[2], spwanPosition, Quaternion.identity);
                        }
                    }
                }
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
