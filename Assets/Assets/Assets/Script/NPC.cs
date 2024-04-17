using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    GameObject Interection;
    GameObject Player;

    private void Start()
    {
        Interection = GetComponent<GameObject>();    
        Player = GetComponent<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Palyer"))
        {
            Player.layer = 13;
            Interection.SetActive(true);
        }
    }
}
