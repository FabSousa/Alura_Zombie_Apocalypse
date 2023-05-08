using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject exitButton;

    public void Awake(){
        #if UNITY_STANDALONE || UNITY_EDITOR
            exitButton.SetActive(true);
        #endif
    }

    public void Play(){
        StartCoroutine(LoadSceneWithDelay(Strings.HotelScene, 0.1f));
    }

    private IEnumerator LoadSceneWithDelay(string scene, float delay){
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(scene);
    }

    public void Quit(){
        StartCoroutine(QuitWithDelay(0.2f));
    }

    private IEnumerator QuitWithDelay(float delay){
        yield return new WaitForSecondsRealtime(delay);
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
