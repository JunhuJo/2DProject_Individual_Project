using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{

    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;

    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("Think", 2);
    }
    void FixedUpdate()
    {
        //몬스터 이동
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

        //절벽 탐색
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
}

