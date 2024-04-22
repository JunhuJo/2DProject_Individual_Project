using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TalkManager : MonoBehaviour
{
    //Dictionary<int, string[]> TalkData; //토크 사전
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
    //    TalkData.Add(1000, new string[] { "마침 잘 왔군!", "이 앞은 막다른 길이네 위쪽으로 돌아서 가야겠어!" });
    //    TalkData.Add(100, new string[] { "몬스터가 많이 포진되어있군", "빠르게 처치 해야겠어!" });
    //}
    //
    //public string GetTalk(int id, int talkIndex)
    //{
    //    return TalkData[id][talkIndex];
    //}
}
