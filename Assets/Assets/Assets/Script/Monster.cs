using UnityEngine;

public class Monster : MonoBehaviour
{
    public  int MonsternowHp = GameManager.MonsterNowHP;
    private int MonsterAP = GameManager.MonsterAP;
    private int PlayerAP = GameManager.PlayerAP;
    public GameObject[] DropItemPrefab;
    public Vector2 spwanPosition;

    public AudioSource source;
    public AudioClip monsterDie;
    public Transform Transform;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        Transform = GetComponent<Transform>();
        source = GetComponent<AudioSource>();
        
        for (int i = 0; i < DropItemPrefab.Length; i++)
        {
            DropItemPrefab[i] = GameObject.FindGameObjectWithTag("Item");
        }
    }

    //몬스터가 HitBox와 충돌 시 처리(대미지)
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
                source.PlayOneShot(monsterDie);
                gameObject.layer = 14;// 몬스터가 죽으면 레이어 변경으로 죽을 때 타격 불가

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
    public void LoadAudioClips()
    {
        monsterDie = Resources.Load<AudioClip>("Audio/monsterDie");
    }

    void MonsterDie()
    {
        gameObject.layer = 6;
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
