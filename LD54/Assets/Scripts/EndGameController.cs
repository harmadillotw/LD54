using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        scoreText.text = "Final Score\n\r" + GlobalValues.People + " people rescued.\n\r\n\r You finished with " + GlobalValues.Resources + " resources and " + GlobalValues.Moral + " Moral";
        if (GlobalValues.lost)
        {
            messageText.text = "Unfortunatley moral has dropped below 0 and you have been relieved of you duties.\n\r Thank you for playing";
        }
        else
        {
            messageText.text = "Thank you for playing";
        }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
