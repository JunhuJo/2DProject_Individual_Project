using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //플레이어 정보
    private int maxHP = GameManager.PlayerMaxHP;
    private int nowHP = GameManager.PlayerNowHP;
    public int PlayerAP = GameManager.PlayerAP;
    public GameObject menu;

    private Animator animator;
    private InputAction attackAction;
    private InputAction DashAction;
    private InputAction teleport;

    public AudioSource Source;
    public AudioClip MonsterHitSound;
    public AudioClip HitSound;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Source = GetComponent<AudioSource>();

        attackAction = new InputAction(binding: "<Keyboard>/a");
        attackAction.started += context => animator.SetTrigger("doAttack");

        DashAction = new InputAction(binding: "<Keyboard>/s");
        DashAction.started += context => animator.SetTrigger("doDashAttack");

        teleport = new InputAction(binding: "<Keyboard>/g");
        teleport.started += context => telport();
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        Source.volume = 0.15f;
        if (other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("몬스터 타격함");
            Source.PlayOneShot(MonsterHitSound);
        }
    }

    public void LoadAudioClips()
    {
        MonsterHitSound = Resources.Load<AudioClip>("Audio/MonsterHitSound");
        HitSound = Resources.Load<AudioClip>("Audio/HitSound");
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
