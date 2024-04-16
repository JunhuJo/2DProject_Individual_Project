using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
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
    public static int PlayerMaxHP = 100;
    public static int PlayerNowHP = 100;
    public static int PlayerAP = 30;

    public static int MonsterNowHP = 100;
    public static int MonsterAP = 10;

    public static int BossMonsterNowHP = 200;
    public static int BossMonsterAP = 15;

    public static int TrapAP = 5;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
