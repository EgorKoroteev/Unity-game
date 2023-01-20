using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    public void ComeBackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void SwitchLV1ToLV2()
    {
        SceneManager.LoadScene(2);
    }

    public void SwitchLV2ToLV3()
    {
        SceneManager.LoadScene(3);
    }

    public void EscapeToMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(4);
        }
    }

    void Update()
    {
        EscapeToMenu();
    }
}