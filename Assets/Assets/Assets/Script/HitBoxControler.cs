using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxControler : MonoBehaviour
{
    private SpriteRenderer ParentspriteRenderer;
    [SerializeField] private float tureHitBoxPosition;
    [SerializeField] private float falseHitBoxPosition;

    //히트박스 캐릭터 기준 위치 검증해서 위치 돌리기
    void Start()
    {
        ParentspriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (ParentspriteRenderer.flipX == true)
        {
            transform.localPosition = new Vector3(tureHitBoxPosition, 0, 0);
        }
        else 
        {
            transform.localPosition = new Vector3(falseHitBoxPosition, 0, 0);
        }
    }
}
