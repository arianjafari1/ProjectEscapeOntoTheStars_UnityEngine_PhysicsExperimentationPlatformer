using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tokensText;
    

    public void Setup(int tokens) //this method will be triggered in another screen to open the game over screen
    {
        
        gameObject.SetActive(true); //this is to actually be able to show this screen
        tokensText.text = "TOKENS: " + tokens.ToString(); // this is what tokens will show on gameover screen
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Game");
    }

    public void MenuButton() //will need to add a main menu later
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitButton()
    {
        Application.Quit();
    }



}
