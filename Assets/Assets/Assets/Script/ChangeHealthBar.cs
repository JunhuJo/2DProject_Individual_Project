using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHealthBar : MonoBehaviour
{
    // 하아...................
    protected float nowHealth;
    public float maxHealth;
    [SerializeField]private Image barimage;

    private void Start()
    {
        Damage(0.3f);
    }
    private void ChangeHealthBarAmonout(float amount)
    {

        barimage.fillAmount = amount;
        if (barimage.fillAmount == 0f || barimage.fillAmount == 1f)
        {
            Debug.Log("숨긴다");
        }
    
    }
    public void SetHp(float amount)
    {
        maxHealth = amount;
        nowHealth = maxHealth;
    }

    public Slider HpBarslider;

    public void CheckHp()
    {
        if (HpBarslider != null)
        {
            HpBarslider.value = nowHealth / maxHealth;
        }
    }
    public void Damage(float damage)
    {
        if (maxHealth == 0 || nowHealth <= 0)
        {
            return;
        }
        nowHealth -= damage;
        CheckHp();
        if (nowHealth <= 0)
        {
            Debug.Log("플레이어 사망");
        }
    
    }
}
