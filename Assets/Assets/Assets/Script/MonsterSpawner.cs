using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] gameObjects;
    
    private void Start()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        while (true)
        {
            int monsterDelay = Random.Range(15, 30);
            yield return new WaitForSeconds(monsterDelay); //랜덤으로 리스폰 시간 돌리기

            for (int i = 0; i < gameObjects.Length; i++)
            {
                GameObject obj = gameObjects[i]; // 배열에서 GameObject 가져오기
                if (!obj.activeSelf) // 비활성화된 상태인지 확인
                {
                    Monster monster = obj.GetComponent<Monster>(); // 해당 GameObject의 Monster 컴포넌트 가져오기
                    if (monster != null && monster.MonsternowHp <= 0)
                    {
                        obj.SetActive(true); // GameObject 활성화
                        monster.MonsternowHp = GameManager.MonsterNowHP;
                    }
                }
            }
        }
    }
}