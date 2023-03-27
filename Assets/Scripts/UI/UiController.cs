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
        int min = (int)(Time.timeSinceLevelLoad / 60);
        int sec = (int)(Time.timeSinceLevelLoad % 60);

        string survivedTime = "Você sobreviveu por ";
        if(min>0)
            survivedTime += $"{min}minutos e ";
        survivedTime += $"{sec}s";

        survivedTimeText.text = survivedTime;

        gameOverUI.SetActive(true);
    }

    public void RestartGame(){
        gm.Restart();
    }

}
