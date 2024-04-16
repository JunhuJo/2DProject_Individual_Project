using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DotHPpotion : MonoBehaviour
{
    private void Update()
    {
        Slot slot = transform.parent.GetComponent<Slot>();

        if (Keyboard.current[Key.Q].wasPressedThisFrame && slot.num + 1 == 1)
        {
            DotRecovery();
            Destroy(gameObject);
        }
        else if (Keyboard.current[Key.W].wasPressedThisFrame && slot.num + 1  == 2)
        {
            DotRecovery();
            Destroy(gameObject);
        }
        else if (Keyboard.current[Key.E].wasPressedThisFrame && slot.num + 1 == 3)
        {
            DotRecovery();
            Destroy(gameObject);
        }
    }

    private void DotRecovery()
    {
        HPBarControl hPBarControl = GameObject.Find("PlayerHPBar").GetComponent<HPBarControl>();

        hPBarControl.nowHp += 50;
        Debug.Log("HP : 50 È¸º¹");
    }
}

