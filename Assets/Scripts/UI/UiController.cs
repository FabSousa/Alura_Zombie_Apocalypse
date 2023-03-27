using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    private GameMode gm;
    private PlayerController player;
    private Stats playerSt;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Slider PlayerHealthSlider;
    [SerializeField] private TextMeshProUGUI survivedTimeText;
    [SerializeField] private TextMeshProUGUI bestSurvivedTimeText;

    private void Awake()
    {
        gm = GameObject.FindWithTag(Strings.CoreScriptsTag).GetComponent<GameMode>();
        player = GameObject.FindWithTag(Strings.PlayerTag).GetComponent<PlayerController>();
        playerSt = player.gameObject.GetComponent<Stats>();

        gameOverUI.SetActive(false);
    }

    private void Start(){
        PlayerHealthSlider.maxValue = playerSt.MaxHealth;
        PlayerHealthSlider.value = playerSt.MaxHealth;
    }

    public void UpdatePlayerHealthSlider(){
        PlayerHealthSlider.value = playerSt.CurrentHealth;
    }

    public void ShowGameOverText(){
        int min = (int) Time.timeSinceLevelLoad / 60;
        int sec = (int) Time.timeSinceLevelLoad % 60;
        survivedTimeText.text = FormatSurvivedTime("Você sobreviveu por", min, sec);
        bestSurvivedTimeText.text = SetBestSurvivedTime(min, sec);
        gameOverUI.SetActive(true);
    }

    private string FormatSurvivedTime(string message, int min, int sec){
        string survivedTime = $"{message} ";
        if(min>0)
            survivedTime += $"{min}minutos e ";
        survivedTime += $"{sec}s";

        return survivedTime;
    }

    private string SetBestSurvivedTime(int min, int sec){
        if(Time.timeSinceLevelLoad > PlayerPrefs.GetFloat(Strings.BestSurvivedTimeSave))
            PlayerPrefs.SetFloat(Strings.BestSurvivedTimeSave, Time.timeSinceLevelLoad);
        else{
            min = (int) PlayerPrefs.GetFloat(Strings.BestSurvivedTimeSave) / 60;
            sec = (int) PlayerPrefs.GetFloat(Strings.BestSurvivedTimeSave) % 60;
        }
        
        return FormatSurvivedTime("Seu melhor tempo foi", min, sec);
    }

    public void RestartGame(){
        gm.Restart();
    }

}
