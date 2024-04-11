using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RestartButton : MonoBehaviour
{
    public TextMeshProUGUI buttonText;

    private void Start()
    {
        // Assuming the button text is showing "Restart"
        if (buttonText != null)
        {
            buttonText.text = "Restart";
        }
    }

    // This method is called when the restart button is clicked
    public void RestartGame()
    {
        // Reload the current scene
        SceneManager.LoadScene("minigame1");
    }
}
