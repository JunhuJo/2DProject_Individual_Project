using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField] private float JumpPower = 9.0f; //������
    private Vector2 inputMovement = Vector2.zero; //��ġ �ʱ�ȭ

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
        // input key�� y�� �������� up�ǰų� isJump �Ķ���Ͱ� false�� ��� ���� �۵�
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
        //���� �� �ٴڿ� �浹 �� Tag�� Grounded�� isJump false�� ����
        if (collision.gameObject.CompareTag("Grounded"))
        {
            animator.SetBool("isJump", false);
        }
        //���� �� �ٴڿ� �浹 �� Tag�� Grounded�̰ų� Ű���� �Է� ���� �ϰ�� �޸���� ����
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
