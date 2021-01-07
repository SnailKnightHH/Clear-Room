using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        StartCoroutine(DelaySceneLoad(1));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Back()
    {
        StartCoroutine(DelaySceneLoad(0));
    }

    public void HowToPlayScene()
    {
        StartCoroutine(DelaySceneLoad(4));
    }

    IEnumerator DelaySceneLoad(int sceneIndex)
    {
        yield return new WaitForSeconds(0.18f);
        SceneManager.LoadScene(sceneIndex);
    }
}
