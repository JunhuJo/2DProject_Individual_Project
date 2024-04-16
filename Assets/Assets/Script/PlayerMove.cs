using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    // ����� ����
    bool PlayerDied = false;

    [SerializeField] private float moveSpeed = 5.0f; // �̵��ӵ�
    private Vector2 inputMovement = Vector2.zero;//��ǥ �ʱ�ȭ.
    private int MonsterAP = GameManager.MonsterAP;
    private int BossMonsterAP = GameManager.BossMonsterAP;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D Rigidbody;
    private HPBarControl hPBarControl;
   
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Rigidbody = GetComponent<Rigidbody2D>();
        hPBarControl = GetComponentInChildren<HPBarControl>();    
    }

    void FixedUpdate()
    {
        //�¿� �̵�
        if (inputMovement.x != 0 && PlayerDied == false)
        {
            animator.SetBool("isIdle", false);
            animator.SetBool("isRun", true);
            animator.SetTrigger("isRun");
            Vector2 moveMovement = inputMovement * moveSpeed * Time.deltaTime;
            transform.Translate(moveMovement);
            Rigidbody.AddForce(moveMovement);

            // ���� ���� �� �ִϸ��̼� ����
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
                animator.SetBool("isRun",false);
            }
        }

        //�������� ������ ����� ���
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
    
   //���� �� �� �ǰ� ����
   void OnCollisionEnter2D(Collision2D collision)
   {
       if (collision.gameObject.CompareTag("Trap") || collision.gameObject.CompareTag("Monster") || collision.gameObject.CompareTag("BossMonster"))
       {
           OnDamaged(collision.transform.position);
          
           //����� �ް� �÷��̾� HP ����
           if (collision.gameObject.CompareTag("Monster"))
           {
               hPBarControl.nowHp -= MonsterAP;
               Debug.Log($"���Ϳ��� ���ظ� {MonsterAP}����");
           }
           else if (collision.gameObject.CompareTag("BossMonster"))
           {
               hPBarControl.nowHp -= BossMonsterAP;
               Debug.Log($"���Ϳ��� ���ظ� {BossMonsterAP}����");
           }
           else if (collision.gameObject.CompareTag("Trap"))
           {
               hPBarControl.nowHp -= GameManager.TrapAP;
               Debug.Log($"�� ���ظ� {GameManager.TrapAP}����");
           }

           //�÷��̾� ���
           if (hPBarControl.nowHp <= 0)
           {
               animator.SetTrigger("isDie");
               gameObject.layer = 12;
               PlayerDied = true;
               spriteRenderer.color = new Color(1, 1, 1, 1);
               StartCoroutine(PlayerDie());
           }
           //�÷��̾� ���� �±׿� ���� �ǰݹ�� ����
           else
           {
                if (collision.gameObject.CompareTag("Monster")|| collision.gameObject.CompareTag("Trap"))
                {
                    gameObject.layer = 11;
                }
                else if(collision.gameObject.CompareTag("BossMonster"))
                {
                    gameObject.layer = 13;
                    spriteRenderer.color = new Color(1, 1, 1, 1);
                }

               //�����·� ����
               Invoke("OffDamaged", 1.5f);
           }
       }
   }
   
   // �ǰ� ���ϸ� ����ȿ�� �� �����ϰ� ����
   void OnDamaged(Vector2 targetPos)
   {
       //�ǰ� ���ϸ� �ǰ� �ִϸ��̼� ���
       animator.SetTrigger("isDamaged");
   
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
   
   //���� ����
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
}

