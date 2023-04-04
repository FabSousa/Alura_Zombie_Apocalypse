using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bossPref;
    [SerializeField] private float timeToSpawn = 60;
    private float timeToNextSpawn;

    void Start()
    {
        timeToNextSpawn = timeToSpawn;
    }

    void Update()
    {
        if(Time.timeSinceLevelLoad >= timeToNextSpawn){
            Instantiate(bossPref, transform.position, Quaternion.identity);
            timeToNextSpawn = timeToSpawn + Time.timeSinceLevelLoad;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1);
    }
}
