using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{

    public Image instructions;
    public GameObject credits;

    // Start is called before the first frame update
    void Start()
    {
        instructions.enabled = false;
        credits.SetActive(false);
        //  StartCoroutine(waitThenMove());
    }

     // this is where the buttons from the main menu do stuff.

    public void Play()
    {
        MusicPlayer.Instance.playUISound();
        SceneManager.LoadScene(1);
        Debug.Log("play");
    }

    public void Instructions()
    {
        MusicPlayer.Instance.playUISound();
        if (instructions.enabled == false) instructions.enabled = true;
        else instructions.enabled = false;
        Debug.Log("Instructions");
    }

    public void Credits()
    {
        MusicPlayer.Instance.playUISound();
        if (credits.activeSelf == false) credits.SetActive(true);
        else credits.SetActive(false);
        Debug.Log("Credits");
    }

    public void Exit()
    {
        MusicPlayer.Instance.playUISound();
        Application.Quit();
        Debug.Log("Exit");
    }
}
