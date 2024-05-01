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

    //ĳ���� & ���� �ɷ�ġ ����
    public static int PlayerMaxHP = 100;
    public static int PlayerNowHP = 100;
    public static int PlayerAP = 30;

    public static int MonsterNowHP = 100;
    public static int MonsterAP = 10;

    public static int BossMonsterNowHP = 400;
    public static int BossMonsterAP = 15;

    public static int TrapAP = 5;
}
    
