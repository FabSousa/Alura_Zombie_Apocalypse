using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMode : MonoBehaviour
{
    private PlayerController pc;
    private UiController uiController;

    void Awake(){
        uiController = GameObject.FindWithTag(Strings.UiTag).GetComponent<UiController>();
        pc = GameObject.FindWithTag(Strings.PlayerTag).GetComponent<PlayerController>();

        Time.timeScale = 1;
    }

    public void GameOver(){
        uiController.ShowGameOverText();
        Time.timeScale = 0;
    }

    public void Restart(){
        SceneManager.LoadScene(Strings.HotelScene);
    }
}
