using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
    public Button singlePlayer, multiPlayer, quit;

    private void Awake()
    {
        AudioManager.instance.Play("IntroTheme");
        AudioManager.instance.Stop("GameTheme");
        singlePlayer.onClick.AddListener(SinglePlayer);
        multiPlayer.onClick.AddListener(MultiPlayer);
        quit.onClick.AddListener(QuitOk);
    }

    public void SinglePlayer()
    {
        AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene(1);
    }

    public void MultiPlayer()
    {
        AudioManager.instance.Play("ButtonClick");
        SceneManager.LoadScene(2);
    }

    public void QuitOk()
    {
        AudioManager.instance.Play("ButtonClick");
        Application.Quit();
    }
}
