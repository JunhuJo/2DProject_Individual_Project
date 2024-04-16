using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxControler : MonoBehaviour
{
    private SpriteRenderer ParentspriteRenderer;
    [SerializeField] private float tureHitBoxPosition;
    [SerializeField] private float falseHitBoxPosition;

    //��Ʈ�ڽ� ĳ���� ���� ��ġ �����ؼ� ��ġ ������
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
