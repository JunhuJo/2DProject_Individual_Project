using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using UnityEditor;

//엔딩 텍스트 페이드 인 효과
public class Ending : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    float Fadetime = 3.0f;
    float currentFadeTime = 0.001f;

    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0.0f);
        StartCoroutine(FadeInText());
    }

    private IEnumerator FadeInText()
    {
        while (currentFadeTime <= Fadetime)
        {
            currentFadeTime += Time.deltaTime;
            text.color = Color.Lerp(Color.black, Color.white, (currentFadeTime / Fadetime));
            yield return null;
        }
        Debug.Log("페이드 인");
        yield return new WaitForSeconds(5);
        Debug.Log("5초 경과");
        text.gameObject.SetActive(false);
    }
}


