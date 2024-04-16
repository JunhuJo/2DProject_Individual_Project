using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    // 사망용 변수
    bool PlayerDied = false;

    [SerializeField] private float moveSpeed = 5.0f; // 이동속도
    private Vector2 inputMovement = Vector2.zero;//좌표 초기화.
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
                animator.SetBool("isRun",false);
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
               animator.SetTrigger("isDie");
               gameObject.layer = 12;
               PlayerDied = true;
               spriteRenderer.color = new Color(1, 1, 1, 1);
               StartCoroutine(PlayerDie());
           }
           //플레이어 몬스터 태그에 따른 피격방식 조건
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

               //원상태로 복귀
               Invoke("OffDamaged", 1.5f);
           }
       }
   }
   
   // 피격 당하면 무적효과 및 투명하게 변경
   void OnDamaged(Vector2 targetPos)
   {
       //피격 당하면 피격 애니메이션 출력
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
}

