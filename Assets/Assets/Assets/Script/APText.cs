using UnityEngine;
using TMPro;

public class APText : MonoBehaviour
{
    public TextMeshProUGUI tmpUgui;
   
    private void Update()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        tmpUgui.text = $"°ø°Ý·Â : {player.PlayerAP}";
    }
}
