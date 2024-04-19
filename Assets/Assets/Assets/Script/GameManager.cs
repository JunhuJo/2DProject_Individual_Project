using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    //�̱���
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

    //ĳ���� & ���� �ɷ�ġ ����
    public static int PlayerMaxHP = 100;
    public static int PlayerNowHP = 100;
    public static int PlayerAP = 30;

    public static int MonsterNowHP = 100;
    public static int MonsterAP = 10;

    public static int BossMonsterNowHP = 400;
    public static int BossMonsterAP = 15;

    public static int TrapAP = 5;
   
    //������ (Playerfs)
    public static void Save()
    {
        Transform playerPos = GameObject.Find("Player").GetComponent<Transform>();
        HPBarControl playerHP = GameObject.Find("PlayerHPBar").GetComponent<HPBarControl>();
        Player playerAP = GameObject.Find("Player").GetComponent<Player>();

        PlayerPrefs.SetFloat("PlayerPositionX", playerPos.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", playerPos.transform.position.y);
        PlayerPrefs.SetInt("PlayerAP", playerAP.PlayerAP);
        PlayerPrefs.SetInt("PlayerHP", playerHP.nowHp);

        Debug.Log("�����");
    }
    
    public void Load()
    {
        Transform playerPos = GameObject.Find("Player").GetComponent<Transform>();
        if (playerPos == null)
        {
            Debug.Log("�÷��̾�Pos�� �����");
        }
        HPBarControl playerHP = GameObject.Find("PlayerHPBar").GetComponent<HPBarControl>();
        if (playerHP == null)
        {
            Debug.Log("�÷��̾�HP�� �����");
        }
        Player playerAP = GameObject.Find("Player").GetComponent<Player>();
        if (playerAP == null)
        {
            Debug.Log("�÷��̾�AP�� �����");
        }


        if (!PlayerPrefs.HasKey("PlayerHP"))
        {
            Debug.Log("����� �����Ͱ� ����");
            return;
        }
        
        float x = PlayerPrefs.GetFloat("PlayerPositionX");
        float y = PlayerPrefs.GetFloat("PlayerPositionY");
        int Playerhp = PlayerPrefs.GetInt("PlayerHP");
        int Playerap = PlayerPrefs.GetInt("PlayerAP");
        
        playerPos.transform.position = new Vector3(x, y, 0);
        playerHP.nowHp = Playerhp;
        playerAP.PlayerAP = Playerap;
        Debug.Log("�ε� �Ϸ�");
    }

    //��ȭ ����
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
