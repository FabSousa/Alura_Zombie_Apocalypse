using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    private PlayerController player;
    private LayerMask zombieMask;
    [SerializeField] private GameObject zombiePref;
    private float spawnTimeCountFloat;
    private int spawnTimeCountInt;
    private int spawnTime;
    [SerializeField][Min(1)] private int minSpawntime = 1, maxSpawntime = 3;
    [SerializeField][Min(1)] private float spawnRadius = 1;
    private float distanceToSpawn = 20;
    
    private void Awake(){
        player = GameObject.FindWithTag(Strings.PlayerTag).GetComponent<PlayerController>();
        zombieMask = LayerMask.GetMask(Strings.ZombieMask);
    }

    private void Update()
    {
        SpawnerCountdown();
    }

    private void SpawnerCountdown(){
        spawnTime = Random.Range(minSpawntime, maxSpawntime+1);
        spawnTimeCountFloat += Time.deltaTime;
        spawnTimeCountInt = (int) (spawnTimeCountFloat % 60);
        if(spawnTimeCountInt >= spawnTime){
            if(Vector3.Distance(player.transform.position, transform.position) >= distanceToSpawn)
                StartCoroutine(SpawnOneZombie());
            spawnTimeCountFloat = 0;
        }
    }

    private IEnumerator SpawnOneZombie(){
        
        Vector3 spawnPos = RandomizePos();
        Collider[] enemiesInArea = Physics.OverlapSphere(spawnPos, spawnRadius, zombieMask);

        while(enemiesInArea.Length > 0){
            spawnPos = RandomizePos();
            enemiesInArea = Physics.OverlapSphere(spawnPos, spawnRadius, zombieMask);
            yield return null;
        }

        Instantiate(zombiePref, spawnPos, transform.rotation);
            
        
    }

    private Vector3 RandomizePos(){
        Vector3 spawnPos = Random.insideUnitSphere * spawnRadius;
        spawnPos += transform.position;
        spawnPos.y = transform.position.y;
        return spawnPos;
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
