using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class APText : MonoBehaviour
{
    public TextMeshProUGUI tmpUgui;
   
    private void Update()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        tmpUgui.text = $"AP : {player.PlayerAP}";
    }
}
