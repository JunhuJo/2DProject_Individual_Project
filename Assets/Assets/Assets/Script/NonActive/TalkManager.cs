using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    //Dictionary<int, string[]> TalkData; //��ũ ����
    public GameObject TextBoxPrefab;
    public Text text;

    void Awake()
    {
        //TalkData = new Dictionary<int, string[]>();
        text = GetComponent<Text>();    
    }

    private void Start()
    {
        GameObject textbox = Instantiate(TextBoxPrefab, TextBoxPrefab.transform);
        
    }

    //void GenerateData()
    //{
    //    TalkData.Add(1000, new string[] { "��ħ �� �Ա�!", "�� ���� ���ٸ� ���̳� �������� ���Ƽ� ���߰ھ�!" });
    //    TalkData.Add(100, new string[] { "���Ͱ� ���� �����Ǿ��ֱ�", "������ óġ �ؾ߰ھ�!" });
    //}
    //
    //public string GetTalk(int id, int talkIndex)
    //{
    //    return TalkData[id][talkIndex];
    //}
}
