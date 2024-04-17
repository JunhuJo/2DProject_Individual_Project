using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.SceneManagement;

//첫번째 엔딩 텍스트이후 다음 출력될 텍스트 페이드 인
public class EndingTextActive : MonoBehaviour
{
    public TMP_Text text;

    float Fadetime = 3.0f;
    float currentFadeTime = 0.001f;
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0.0f);
        StartCoroutine(TextActive());
    }

    private IEnumerator TextActive()
    {
        yield return new WaitForSeconds(8);
        while (currentFadeTime <= Fadetime)
        {
            currentFadeTime += Time.deltaTime;
            text.color = Color.Lerp(Color.black, Color.white, (currentFadeTime / Fadetime));
            yield return null;
        }
        Debug.Log("엔딩 문구");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("StartScene");
    }
}
