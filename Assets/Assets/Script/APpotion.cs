using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class APpotion : MonoBehaviour
{
    private void Update()
    {
        Slot slot = transform.parent.GetComponent<Slot>();

        if (Keyboard.current[Key.Q].wasPressedThisFrame && slot.num + 1 == 1)
        {
            AttackPointUP();
            Destroy(gameObject);
        }
        else if (Keyboard.current[Key.W].wasPressedThisFrame && slot.num + 1  == 2)
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
}
