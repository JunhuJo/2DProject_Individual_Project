using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public enum ButtonType
{ 
    New,
    Exit
}

public class StartMainScreen : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public ButtonType crrentType;
    public Transform buttonsScale;
    Vector3 defaultScale;

    private void Start()
    {
        defaultScale = buttonsScale.localScale;
    }

    //���� ȭ�� ��ư ���� ����
    public void OnBtnClick()
    {
        switch (crrentType)
        {
            case ButtonType.New:
                Debug.Log("������");
                LoadGameScene();
                break;
            case ButtonType.Exit:
                Debug.Log("�����ϱ�");
                Application.Quit();
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonsScale.localScale = defaultScale * 1.05f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonsScale.localScale = defaultScale;
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Loading");
    }

    public void LoadPlayerSceneAsync()
    {
        StartCoroutine(LoadPlayerSceneAsyncCoroutine());
    }

    private IEnumerator LoadPlayerSceneAsyncCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GamePlayerScene");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
