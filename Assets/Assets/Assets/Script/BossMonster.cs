using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossMonster : MonoBehaviour
{
    private int BossMonsterNowHp = GameManager.BossMonsterNowHP;
    private int BossMonsterAP = GameManager.BossMonsterAP;
    private int playerAP = GameManager.PlayerAP;

    public Transform target;

    public float attackDelay = 3f;
    
    Animator anim;
    public AudioSource source;
    public AudioClip bossDie;
    public AudioClip AttackSound;
   
    private void Awake()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
       
    }
    private void Start()
    {
        Debug.Log($"현재 보스몹 체력 : {BossMonsterNowHp}");
    }

    private void Update()
    {
        StartCoroutine(BossAttack());
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
                source.volume = 0.5f;
                source.PlayOneShot(bossDie);
                anim.SetBool("MonsterDie", true);
                Invoke("MonsterDie", 3.0f);
                Invoke("Ending", 7);
            }
        }
    }
    public void LoadAudioClips()
    {
        AttackSound = Resources.Load<AudioClip>("Audio/AttackSound");
        bossDie = Resources.Load<AudioClip>("Audio/bossDie");
    }
    private IEnumerator BossAttack()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        Slider PlayerHp = GameObject.Find("PlayerHPBar").GetComponent<Slider>();

        if (player.transform.position.x > transform.position.x - 2)
        {
            anim.SetBool("BossBaseAttack", true);
            Debug.Log("보스가 공격했다");
            
            if (PlayerHp.value <= 0)
            {
                anim.SetBool("BossBaseAttack", false);
                anim.SetBool("PlayerDied", true);
                Debug.Log("플레이어가 사망해서 보스가 공격을 멈췄다");
            }
            yield return new WaitForSeconds(3);
        }
        else
        {
            anim.SetBool("BossBaseAttack", false);
        }

    }

    void BossIdle()
    {
        anim.SetBool("BossBaseAttack", false);
        Invoke("Ending",5);
    }
    void MonsterDie()
    {
        gameObject.SetActive(false);
    }

    void Ending()
    {
        SceneManager.LoadScene("Ending");
    }

    //보스 몬스터 히트박스 활성/비활성 메서드(애니메이션에 할당)
    void PlayerHitTrue()
    {
        Transform childrenTransform = transform.Find("BossHitBox");
        source.PlayOneShot(AttackSound);
        childrenTransform.gameObject.SetActive(true);
    }
    void PlayerHitFalse()
    {
        Transform childrenTransform = transform.Find("BossHitBox");
        childrenTransform.gameObject.SetActive(false);
    }
}
