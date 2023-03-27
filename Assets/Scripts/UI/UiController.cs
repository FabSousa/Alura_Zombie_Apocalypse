using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private Slider PlayerHealthSlider;
    private PlayerController player;
    private Stats playerSt;

    private void Awake()
    {
        player = GameObject.FindWithTag(Strings.PlayerTag).GetComponent<PlayerController>();
        playerSt = player.gameObject.GetComponent<Stats>();

        gameOverText.SetActive(false);
    }

    private void Start(){
        PlayerHealthSlider.maxValue = playerSt.MaxHealth;
        PlayerHealthSlider.value = playerSt.MaxHealth;
    }

    public void UpdatePlayerHealthSlider(){
        PlayerHealthSlider.value = playerSt.CurrentHealth;
    }

    public void ShowGameOverText(){
        gameOverText.SetActive(true);
    }

}
