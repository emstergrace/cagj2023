using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingPanelUI : MonoBehaviour
{
    public GameObject happyPanel;
    public GameObject neutralPanel;
    public GameObject sadPanel;
    public GameObject menuButton;

	private void Start() {
        CoffeeManager.Inst.endingPanel = this;
	}

	public void OpenMainMenu() {
        SceneManager.LoadScene("Menu");
	}

    public void QuitGame() {
        Application.Quit();
	}
}
