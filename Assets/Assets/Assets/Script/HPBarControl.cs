using UnityEngine;
using UnityEngine.UI;

public class HPBarControl : MonoBehaviour
{
    private Slider slider;
    
    private int maxHp = GameManager.PlayerMaxHP;
    public int nowHp = GameManager.PlayerNowHP;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        slider.value = (float)nowHp / maxHp;
        if (nowHp > 100)
        {
            nowHp = 100;
        }
        else if (nowHp <= 0)
        {
            nowHp = 0;
        }
    }
}
