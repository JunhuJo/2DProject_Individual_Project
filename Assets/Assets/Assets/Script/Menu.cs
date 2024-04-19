using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum MenuButtonType
{ 
    StartScreen,
    Contiune,
    Exit
}

public class Menu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public MenuButtonType crrentType;
    public Transform buttonsScale;
    Vector3 defaultScale;

    private void Start()
    {
        defaultScale = buttonsScale.localScale;
    }

    public void OnMenuBtnClick()
    {
        switch (crrentType)
        { 
            case MenuButtonType.StartScreen:
                Debug.Log("����ȭ������");
                StartGameLoad();
                break;
            case MenuButtonType.Contiune:
                Debug.Log("����ϱ�");
                MenuClose();
                break;
            case MenuButtonType.Exit:
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

    public void MenuClose()
    {
        GameObject menu = GameObject.Find("MenuWindow");
        menu.SetActive(false);
    }

    public void StartGameLoad()
    {
        SceneManager.LoadScene("StartScene");
    }
}
