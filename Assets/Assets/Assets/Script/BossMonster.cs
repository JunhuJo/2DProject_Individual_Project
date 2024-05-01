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
        Debug.Log($"���� ������ ü�� : {BossMonsterNowHp}");
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

            Debug.Log($"���Ͱ� {Player.PlayerAP} �� Ÿ���� ����");
            Debug.Log($"���� HP : {BossMonsterNowHp}");

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
            Debug.Log("������ �����ߴ�");
            
            if (PlayerHp.value <= 0)
            {
                anim.SetBool("BossBaseAttack", false);
                anim.SetBool("PlayerDied", true);
                Debug.Log("�÷��̾ ����ؼ� ������ ������ �����");
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

    //���� ���� ��Ʈ�ڽ� Ȱ��/��Ȱ�� �޼���(�ִϸ��̼ǿ� �Ҵ�)
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
