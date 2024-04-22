using UnityEngine;

public class Monster : MonoBehaviour
{
    //몬스터 정보 게임 매니저에서 받아옴
    public  int MonsternowHp = GameManager.MonsterNowHP;
    private int MonsterAP = GameManager.MonsterAP;
    private int PlayerAP = GameManager.PlayerAP;
    public GameObject[] DropItemPrefab;
    public Vector2 spwanPosition;
    
    //몬스터 사운드
    public AudioSource source;
    public AudioClip monsterDie;
    
    //몬스터 이동
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    public int nextMove;

    public Transform Transform;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 2);
    }

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

    void FixedUpdate()
    {
        //랜덤하게 몬스터 이동
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        if (nextMove == -1)
        {
            spriteRenderer.flipX = true;
            anim.SetBool("MonsterMove", true);
        }
        else if (nextMove == 1)
        {
            spriteRenderer.flipX = false;
            anim.SetBool("MonsterMove", true);
        }
        else
        {
            anim.SetBool("MonsterMove", false);
            anim.SetBool("Idle", true);
        }

        //절벽 탐색 (Ray를 몬스터 앞에 세워두고 바닥과 충돌 감지가 없으면 방향을 틀어 절벽에서 떨어짐 방지)
        float yOffset = 0.7f;
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove, rigid.position.y - yOffset);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Collider"));
        if (rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", 2);
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);
        Invoke("Think", 2);
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
                

                anim.SetBool("MonsterDie", true);
                gameObject.layer = 14;// 몬스터가 죽으면 레이어 변경으로 죽을 때 타격 불가
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
                 if (anim.GetBool("MonsterDie") == true) { Invoke("MonsterDie", 0.7f); }
            }
        }
    }

    public void LoadAudioClips()
    {
        monsterDie = Resources.Load<AudioClip>("Audio/monsterDie");
    }

    void MonsterDie()
    {
        gameObject.SetActive(false);
        gameObject.layer = 6;
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
