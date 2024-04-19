using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> TalkData; //��ũ ����
    public GameObject TextBoxPrefab;
    Text text;

    void Awake()
    {
        TalkData = new Dictionary<int, string[]>();
        text = GetComponent<Text>();    
        
        GenerateData();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            TextBoxPrefab = Instantiate(TextBoxPrefab);
            text.text = GetTalk(1000, 2);
        }
        else if (collision.CompareTag("Start"))
        {
           text.text = GetTalk(100, 2);
        }
    }

    void GenerateData()
    {
        TalkData.Add(1000, new string[] { "��ħ �� �Ա�!", "�� ���� ���ٸ� ���̳� �������� ���Ƽ� ���߰ھ�!" });
        TalkData.Add(100, new string[] { "���Ͱ� ���� �����Ǿ��ֱ�", "������ óġ �ؾ߰ھ�!" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        return TalkData[id][talkIndex];
    }
}
