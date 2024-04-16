using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class Player : MonoBehaviour
{
    //플레이어 정보
    private int maxHP = GameManager.PlayerMaxHP;
    private int nowHP = GameManager.PlayerNowHP;
    public int PlayerAP = GameManager.PlayerAP;
    
    private Animator animator;
    private InputAction attackAction;
    private InputAction DashAction;
    private InputAction teleport;

    private void Awake()
    {
        animator = GetComponent<Animator>();

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

    private void telport()
    {
        transform.position = new Vector2(63, 2.62f);
        Debug.Log("텔포");
    }

    //몬스터 공격 로직
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("몬스터 타격함");
        }
    }

    void MonsterHitTrue()
    {
        Transform childrenTransform = transform.Find("HitBox");
        childrenTransform.gameObject.SetActive(true);
    }
    void MonsterHitFalse()
    {
        Transform childrenTransform = transform.Find("HitBox");
        childrenTransform.gameObject.SetActive(false);
    }
 
}
