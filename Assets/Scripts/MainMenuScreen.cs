using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{


    public void ExitButton()
    {
        Application.Quit();
        //Debug.Log("Game closed");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void MuteGame(bool muted)
    {
        if (muted)
        {
            AudioListener.pause = true;
        }
        else
        {
            AudioListener.pause = false;
        }
    }


}
