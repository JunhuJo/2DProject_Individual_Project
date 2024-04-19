using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public enum ButtonType
{ 
    New,
    Continue,
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

    //시작 화면 버튼 선택 로직
    public void OnBtnClick()
    {
        switch (crrentType)
        {
            case ButtonType.New:
                Debug.Log("새게임");
                LoadGameScene();
                break;
            case ButtonType.Continue:
                Debug.Log("이어하기");
                GameLoad();
                break;
            case ButtonType.Exit:
                Debug.Log("종료하기");
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

    public void GameLoad()
    {
        GameManager.Instance.Load();
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

        // 플레이어가 있는 씬을 로드한 후에 GameManager의 Load 메서드 호출
        GameManager.Instance.Load();
    }
}
