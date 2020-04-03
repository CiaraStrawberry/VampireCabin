using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsManager : MonoBehaviour
{
    // gives a script for the in-editor button to call.
    public void StartGame()
    {
        MusicPlayer.Instance.playUISound();
        SceneManager.LoadScene(2);
    }
}
