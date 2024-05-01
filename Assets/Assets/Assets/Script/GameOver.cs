using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(gameOver());
    }

    private IEnumerator gameOver()
    {
        yield return new WaitForSeconds(7);
        SceneManager.LoadScene("GameOverLoading");
    }
}
