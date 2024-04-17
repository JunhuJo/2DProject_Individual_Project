using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.SceneManagement;

//ù��° ���� �ؽ�Ʈ���� ���� ��µ� �ؽ�Ʈ ���̵� ��
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
        Debug.Log("���� ����");
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("StartScene");
    }
}
