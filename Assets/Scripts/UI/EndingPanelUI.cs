using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingPanelUI : MonoBehaviour
{
    
    public void OpenMainMenu() {
        SceneManager.LoadScene("Menu");
	}

    public void QuitGame() {
        Application.Quit();
	}
}
