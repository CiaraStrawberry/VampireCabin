using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This class hadles the players health and the icons linked to that.
/// </summary>
public class PlayerHealthManager : MonoBehaviour
{
    public int playerHealth;
    public Image[] hearts;


    [ContextMenu("lose health")]
    public void loseHealth()
    {
        if (playerHealth == -1) return;

        hearts[playerHealth].enabled = false;
        playerHealth--;
        Debug.Log(playerHealth);
        if (playerHealth == -1)
        {
            MusicPlayer.Instance.playDeath();
            dead();

        }
        else
        {
            MusicPlayer.Instance.playPain();
        }
    }

    private void dead ()
    {
        Debug.Log("you have died");
        GlobalVariables.Instance.sceneMan.die();
    }
}
