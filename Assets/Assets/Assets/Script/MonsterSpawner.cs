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
            yield return new WaitForSeconds(monsterDelay); //�������� ������ �ð� ������

            for (int i = 0; i < gameObjects.Length; i++)
            {
                GameObject obj = gameObjects[i]; // �迭���� GameObject ��������
                if (!obj.activeSelf) // ��Ȱ��ȭ�� �������� Ȯ��
                {
                    Monster monster = obj.GetComponent<Monster>(); // �ش� GameObject�� Monster ������Ʈ ��������
                    if (monster != null && monster.MonsternowHp <= 0)
                    {
                        obj.SetActive(true); // GameObject Ȱ��ȭ
                        monster.MonsternowHp = GameManager.MonsterNowHP;
                    }
                }
            }
        }
    }
}