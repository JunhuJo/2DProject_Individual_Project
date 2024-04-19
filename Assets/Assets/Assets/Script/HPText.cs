using UnityEngine;
using TMPro;

public class HPText : MonoBehaviour
{
    public TextMeshProUGUI tmpUgui;
    
    private void Update()
    {
        HPBarControl PlayerHP = GameObject.Find("PlayerHPBar").GetComponent<HPBarControl>();
        tmpUgui.text = $"{PlayerHP.nowHp} / 100";
    }
}
