using UnityEngine;

public class MonsterHPBarPosition : MonoBehaviour
{
    public float YValue = 0.75f;
    public float XValue = 0.15f;

    void Update()
    {
        Vector2 HPposition = new Vector2(gameObject.transform.position.x + XValue, gameObject.transform.position.y + YValue);
        this.transform.position = HPposition;
    }
}
