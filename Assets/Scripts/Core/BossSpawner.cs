using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bossPref;
    [SerializeField] private float timeToSpawn = 60;
    [SerializeField] private Transform[] spawnPoint;
    private UiController uiController;
    private Transform playerPos;
    private float timeToNextSpawn;
    private float warningTextDuration = 1;
    private float warningTextFadeDuration = 3;

    private void Awake(){
        uiController = GameObject.FindObjectOfType<UiController>() as UiController;
        playerPos = GameObject.FindWithTag(Strings.PlayerTag).transform;
    }

    private void Start()
    {
        timeToNextSpawn = timeToSpawn;
    }

    private void Update()
    {
        if(Time.timeSinceLevelLoad >= timeToNextSpawn){
            uiController.ShowBossSpawnWarning(warningTextDuration, warningTextFadeDuration);
            Instantiate(bossPref, FindSpawnPoint(), Quaternion.identity);
            timeToNextSpawn = timeToSpawn + Time.timeSinceLevelLoad;
        }
    }

    private Vector3 FindSpawnPoint(){
        Vector3 spawn = Vector3.zero;
        float greaterDistance = 0;
        foreach (Transform pos in spawnPoint)
        {
            float distance = Vector3.Distance(playerPos.position, pos.position);
            if(distance > greaterDistance){
                greaterDistance = distance;
                spawn = pos.position;
            }
        }
        return spawn;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
