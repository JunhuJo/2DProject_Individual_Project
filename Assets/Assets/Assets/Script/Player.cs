using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject npc;
    public GameObject textBox;
    
    // 사망용 변수
    bool PlayerDied = false;

    //이동
    [SerializeField] private float moveSpeed = 5.0f; // 이동속도
    private Vector2 inputMovement = Vector2.zero;//좌표 초기화.
    private int MonsterAP = GameManager.MonsterAP;
    private int BossMonsterAP = GameManager.BossMonsterAP;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D Rigidbody;
    private HPBarControl hPBarControl;

    //공격 효과음
    public AudioSource Source;
    public AudioClip DieSonud;
    public AudioClip MonsterHitSound;
    public AudioClip HitSound;

    //플레이어 정보
    private int maxHP = GameManager.PlayerMaxHP;
    private int nowHP = GameManager.PlayerNowHP;
    public int PlayerAP = GameManager.PlayerAP;
    public GameObject menu;

    private InputAction attackAction;
    private InputAction DashAction;
    private InputAction teleport;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Source = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();
        hPBarControl = GetComponentInChildren<HPBarControl>();

        attackAction = new InputAction(binding: "<Keyboard>/a");
        attackAction.started += context => animator.SetTrigger("doAttack");

        DashAction = new InputAction(binding: "<Keyboard>/s");
        DashAction.started += context => animator.SetTrigger("doDashAttack");

    }

    private void Start()
    {
        Debug.Log($"현재 플레이어 공격력 : {PlayerAP}");
    }

    private void Update()
    {
        if (Keyboard.current[Key.T].wasPressedThisFrame)
        {
            telport();
        }

        if(Keyboard.current[Key.Escape].wasPressedThisFrame)
        {
            MenuOpen();
        }

        if (Keyboard.current[Key.Y].wasPressedThisFrame)
        {
            NPCteleport();
        }

        if (Keyboard.current[Key.X].wasPressedThisFrame && npc.CompareTag("NPC"))
        {
            StartCoroutine(NPCtext());
        }
    }

    IEnumerator NPCtext()
    {
        textBox.SetActive(true);
        yield return new WaitForSeconds(4);
        textBox.SetActive(false);
    }

    void FixedUpdate()
    {
        //좌우 이동
        if (inputMovement.x != 0 && PlayerDied == false)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isRun", true);
            animator.SetTrigger("isRun");

            Vector2 moveMovement = inputMovement * moveSpeed * Time.deltaTime;
            transform.Translate(moveMovement);
            Rigidbody.AddForce(moveMovement);

            // 방향 변경 시 애니메이션 반전
            if (inputMovement.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (inputMovement.x < 0)
            {
                spriteRenderer.flipX = true;
            }

            if (inputMovement.y > 0)
            {
                animator.SetBool("isRun", false);
            }
        }

        //움직임이 없으면 대기모션 출력
        else if (inputMovement.x == 0 && PlayerDied == false)
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isIdle", true);
        }
    }

    void OnMove(InputValue inputValue)
    {
        inputMovement = inputValue.Get<Vector2>();
    }

    private void OnEnable()
    {
        attackAction.Enable();
        DashAction.Enable();
    }

    private void OnDisable()
    {
        attackAction.Disable();
        DashAction.Disable();
    }

    //해당 좌표로 순간이동
    private void telport()
    {
        transform.position = new Vector2(63, 2.62f);
        Debug.Log("텔포");
    }

    private void NPCteleport()
    {
        transform.position = new Vector2(45.48465f, 1.44459f);
        Debug.Log("NPC텔포");
    }

    //메뉴창 오픈
    private void MenuOpen()
    {
       menu.SetActive(true);
    }

    //몬스터 공격 로직
    void OnTriggerEnter2D(Collider2D other)
    {
        Source.volume = 0.15f;
        if (other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("몬스터 타격함");
            Source.PlayOneShot(MonsterHitSound);
        }
    }

    //몬스터 및 덫 피격 판정
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Monster") || collision.gameObject.CompareTag("BossMonster"))
        {
            OnDamaged(collision.transform.position);

            //대미지 받고 플레이어 HP 갱신
            if (collision.gameObject.CompareTag("Monster"))
            {
                hPBarControl.nowHp -= MonsterAP;
                Debug.Log($"몬스터에게 피해를 {MonsterAP}입음");
            }
            else if (collision.gameObject.CompareTag("BossMonster"))
            {
                hPBarControl.nowHp -= BossMonsterAP;
                Debug.Log($"몬스터에게 피해를 {BossMonsterAP}입음");
            }
            else if (collision.gameObject.CompareTag("Trap"))
            {
                hPBarControl.nowHp -= GameManager.TrapAP;
                Debug.Log($"덫 피해를 {GameManager.TrapAP}입음");
            }

            //플레이어 사망
            if (hPBarControl.nowHp <= 0)
            {
                Source.volume = 1;
                Source.PlayOneShot(DieSonud);
                animator.SetBool("isRun", false);
                animator.SetTrigger("isDie");
                gameObject.layer = 13;
                PlayerDied = true;
                spriteRenderer.color = new Color(1, 1, 1, 1);
                StartCoroutine(PlayerDie());
            }
            //플레이어 몬스터 태그에 따른 피격방식 조건
            else
            {
                if (collision.gameObject.CompareTag("Monster") || collision.gameObject.CompareTag("Trap"))
                {
                    gameObject.layer = 11;
                }
                else if (collision.gameObject.CompareTag("BossMonster"))
                {
                    gameObject.layer = 12;
                    spriteRenderer.color = new Color(1, 1, 1, 1);
                }
                //원상태로 복귀
                Invoke("OffDamaged", 1.5f);
            }
        }
    }

    void OnDamaged(Vector2 targetPos)
    {
        //피격 당하면 피격 애니메이션 출력
        animator.SetTrigger("isDamaged");
        
        // 피격 당하면 무적효과 및 투명하게 변경
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);

        if (spriteRenderer.flipX == false)
        {
            Rigidbody.AddForce(new Vector2(-1, 1) * 4, ForceMode2D.Impulse);
        }
        else
        {
            Rigidbody.AddForce(new Vector2(1, 1) * 4, ForceMode2D.Impulse);
        }
    }

    //무적 해제
    void OffDamaged()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    private IEnumerator PlayerDie()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("GameOver");
    }

    public void LoadAudioClips()
    {
        MonsterHitSound = Resources.Load<AudioClip>("Audio/MonsterHitSound");
        HitSound = Resources.Load<AudioClip>("Audio/HitSound");
        DieSonud = Resources.Load<AudioClip>("Audio/DieSonud");
    }

    void MonsterHitTrue()
    {
        Transform childrenTransform = transform.Find("HitBox");
        Source.PlayOneShot(HitSound);
        childrenTransform.gameObject.SetActive(true);
    }
    void MonsterHitFalse()
    {
        Transform childrenTransform = transform.Find("HitBox");
        childrenTransform.gameObject.SetActive(false);
    }
}
