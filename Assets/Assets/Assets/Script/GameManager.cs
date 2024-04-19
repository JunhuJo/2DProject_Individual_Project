using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    //싱글톤
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject gameManagerObject = new GameObject("GameManager");
                    instance = gameManagerObject.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    //public static bool GameStartType = false;

    //캐릭터 & 몬스터 능력치 정보
    public static int PlayerMaxHP = 100;
    public static int PlayerNowHP = 100;
    public static int PlayerAP = 30;

    public static int MonsterNowHP = 100;
    public static int MonsterAP = 10;

    public static int BossMonsterNowHP = 400;
    public static int BossMonsterAP = 15;

    public static int TrapAP = 5;
   
    //저장기능 (Playerfs)
    public static void Save()
    {
        Transform playerPos = GameObject.Find("Player").GetComponent<Transform>();
        HPBarControl playerHP = GameObject.Find("PlayerHPBar").GetComponent<HPBarControl>();
        Player playerAP = GameObject.Find("Player").GetComponent<Player>();

        PlayerPrefs.SetFloat("PlayerPositionX", playerPos.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", playerPos.transform.position.y);
        PlayerPrefs.SetInt("PlayerAP", playerAP.PlayerAP);
        PlayerPrefs.SetInt("PlayerHP", playerHP.nowHp);

        Debug.Log("저장됨");
    }
    
    public void Load()
    {
        Transform playerPos = GameObject.Find("Player").GetComponent<Transform>();
        if (playerPos == null)
        {
            Debug.Log("플레이어Pos가 비었음");
        }
        HPBarControl playerHP = GameObject.Find("PlayerHPBar").GetComponent<HPBarControl>();
        if (playerHP == null)
        {
            Debug.Log("플레이어HP가 비었음");
        }
        Player playerAP = GameObject.Find("Player").GetComponent<Player>();
        if (playerAP == null)
        {
            Debug.Log("플레이어AP가 비었음");
        }


        if (!PlayerPrefs.HasKey("PlayerHP"))
        {
            Debug.Log("저장된 데이터가 없음");
            return;
        }
        
        float x = PlayerPrefs.GetFloat("PlayerPositionX");
        float y = PlayerPrefs.GetFloat("PlayerPositionY");
        int Playerhp = PlayerPrefs.GetInt("PlayerHP");
        int Playerap = PlayerPrefs.GetInt("PlayerAP");
        
        playerPos.transform.position = new Vector3(x, y, 0);
        playerHP.nowHp = Playerhp;
        playerAP.PlayerAP = Playerap;
        Debug.Log("로드 완료");
    }

    //대화 정보
    public TalkManager TalkManager;
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    public int talkIndex;

    public void Action(GameObject scanObj)
    {
        if (isAction)
        {
            isAction = false;
        }
        else 
        {
            isAction=true;
            scanObject = scanObj;
            ObjectData objData =  scanObject.GetComponent<ObjectData>();
            Talk(objData.id, objData.isNpc);
        }
        talkPanel.SetActive(isAction);
    }

    void Talk(int id, bool isNpc)
    {
       string talkData =  TalkManager.GetTalk(id, talkIndex);

        if (isNpc)
        {
            talkText.text = talkData;
        }
        else
        {
            talkText.text = talkData;
        }
    }
}
