using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

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

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Loading");
    }

}
