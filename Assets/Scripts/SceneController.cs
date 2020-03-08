using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class controls the movement betweens scenes and the winning and losing screens.
/// </summary>
public class SceneController : MonoBehaviour
{

    public GameObject deathScreen;

    public GameObject winScreen;

    public bool gameAlreadyFinished;

    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);
        winScreen.SetActive(false);
    }

    public void die ()
    {
        if (!gameAlreadyFinished)
        {
            gameAlreadyFinished = true;
            StartCoroutine(waitForASecBeforeDeath());
        }
    }

    public void win ()
    {
        if (!gameAlreadyFinished)
        {
            gameAlreadyFinished = true;
            StartCoroutine(waitForASecBeforeWin());
        }
    }

    IEnumerator waitForASecBeforeDeath()
    {
        yield return new WaitForSeconds(3);
        deathScreen.SetActive(true);
    }

    IEnumerator waitForASecBeforeWin()
    {
        yield return new WaitForSeconds(3);
        winScreen.SetActive(true);
    }

    [ContextMenu("return to the menu")]
    public void returnToTheMenu ()
    {
        Destroy(GlobalVariables.Instance);
        SceneManager.LoadScene(0);   
    }
}
