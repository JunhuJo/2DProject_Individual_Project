using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HPpotion : MonoBehaviour
{
    private void Update()
    {
        Slot slot = transform.parent.GetComponent<Slot>();

        if (Keyboard.current[Key.Q].wasPressedThisFrame && slot.num + 1 == 1)
        {
            Recovery();
            Destroy(gameObject);
        }
        else if (Keyboard.current[Key.W].wasPressedThisFrame && slot.num + 1  == 2)
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
        Debug.Log($"HP : {20} È¸º¹");
    }
}
