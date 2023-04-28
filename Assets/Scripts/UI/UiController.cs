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
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private TextMeshProUGUI bossSpawnWarningText;
    private int killCount = 0;

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

    public void UpdateKillCount(){
        killCount++;
        killCountText.text = $"X {killCount}";
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
        if(min>0){
            if(min > 1)
                survivedTime += $"{min} minutos e ";
            else
                survivedTime += $"{min} minuto e ";
        }
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

    public void ShowBossSpawnWarning(float duration, float fadeDuration){
        StartCoroutine(ShowTempText(bossSpawnWarningText, duration, fadeDuration));
        }

    private IEnumerator ShowTempText(TextMeshProUGUI text, float duration, float fadeDuration){
        text.gameObject.SetActive(true);
        Color textColor = text.color;
        textColor.a = 1;
        text.color = textColor;
        yield return new WaitForSeconds(duration);
        float count = 0;
        while(text.color.a > 0){
            count += (Time.deltaTime / fadeDuration);
            textColor.a = Mathf.Lerp(1, 0, count);
            text.color = textColor;
            yield return null;
        }
        text.gameObject.SetActive(false);
    }
}
