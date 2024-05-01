using UnityEngine;
using UnityEngine.InputSystem;

public class UseItem : MonoBehaviour
{
    public int ItemKey;

    private void Update()
    {
        if (ItemKey == 100) { apPotion(); }
        else if (ItemKey == 200) { hpPotion(); }
        else if (ItemKey == 300) { maxHppotion(); }
    }

    //AP물약
    void apPotion()
    {
        Slot slot = transform.parent.GetComponent<Slot>();

        if (Keyboard.current[Key.Q].wasPressedThisFrame && slot.num + 1 == 1)
        {
            AttackPointUP();
            Destroy(gameObject);
        }
        else if (Keyboard.current[Key.W].wasPressedThisFrame && slot.num + 1 == 2)
        {
            AttackPointUP();
            Destroy(gameObject);
        }
        else if (Keyboard.current[Key.E].wasPressedThisFrame && slot.num + 1 == 3)
        {
            AttackPointUP();
            Destroy(gameObject);
        }
    }
    private void AttackPointUP()
    {
        Player Player = GameObject.Find("Player").GetComponent<Player>();
        Player.PlayerAP += 10;
        Debug.Log($"공격력 : 10 상승");
        Debug.Log($"플레이어 공격력 : {Player.PlayerAP}");
    }

    //20 HP물약
    void hpPotion()
    {
        Slot slot = transform.parent.GetComponent<Slot>();

        if (Keyboard.current[Key.Q].wasPressedThisFrame && slot.num + 1 == 1)
        {
            Recovery();
            Destroy(gameObject);
        }
        else if (Keyboard.current[Key.W].wasPressedThisFrame && slot.num + 1 == 2)
        {
            Recovery();
            Destroy(gameObject);
        }
        else if (Keyboard.current[Key.E].wasPressedThisFrame && slot.num + 1 == 3)
        {
            Recovery();
            Destroy(gameObject);
        }
    }
    private void Recovery()
    {
        HPBarControl hPBarControl = GameObject.Find("PlayerHPBar").GetComponent<HPBarControl>();
        hPBarControl.nowHp += 20;
        Debug.Log($"HP : {20} 회복");
    }


    //대량 HP 물약
    void maxHppotion()
    {
        Slot slot = transform.parent.GetComponent<Slot>();

        if (Keyboard.current[Key.Q].wasPressedThisFrame && slot.num + 1 == 1)
        {
            MaxRecovery();
            Destroy(gameObject);
        }
        else if (Keyboard.current[Key.W].wasPressedThisFrame && slot.num + 1 == 2)
        {
            MaxRecovery();
            Destroy(gameObject);
        }
        else if (Keyboard.current[Key.E].wasPressedThisFrame && slot.num + 1 == 3)
        {
            MaxRecovery();
            Destroy(gameObject);
        }
    }

    private void MaxRecovery()
    {
        HPBarControl hPBarControl = GameObject.Find("PlayerHPBar").GetComponent<HPBarControl>();

        hPBarControl.nowHp += 50;
        Debug.Log("HP : 50 회복");
    }

}
