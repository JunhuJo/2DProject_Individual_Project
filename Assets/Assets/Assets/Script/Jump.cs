using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField] private float JumpPower = 9.0f; //점프력
    private Vector2 inputMovement = Vector2.zero; //위치 초기화

    private Animator animator;
    private Rigidbody2D Rigidbody;
    private Player player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    void FixedUpdate()
    {
        // input key가 y축 방향으로 up되거나 isJump 파라미터가 false일 경우 점프 작동
        if (inputMovement.y > 0 && !animator.GetBool("isJump") && player.gameObject.layer != 13)
        {
            animator.SetBool("isRun", false);
            animator.SetTrigger("isJump");
            animator.SetBool("isJump", true);
            
            transform.Translate(new Vector2(0,2)* JumpPower * Time.deltaTime);
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, JumpPower);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //점프 후 바닥에 충돌 시 Tag가 Grounded면 isJump false로 변경
        if (collision.gameObject.CompareTag("Grounded"))
        {
            animator.SetBool("isJump", false);
        }
        //점프 후 바닥에 충돌 시 Tag가 Grounded이거나 키보드 입력 상태 일경우 달리기로 변경
        else if (collision.gameObject.CompareTag("Grounded") && Keyboard.current.IsPressed())
        {
            animator.SetBool("isJump", false);
            animator.SetBool("isRun", true);
        }
    }

    private void OnJump(InputValue inputValue)
    {
        inputMovement = inputValue.Get<Vector2>();
    }
}
