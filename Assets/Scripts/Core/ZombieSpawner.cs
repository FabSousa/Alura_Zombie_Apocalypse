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
    private int maxZombiesAlive = 2;
    private int zombiesAlive;
    private float timeForRiseDifficulty = 15;
    private float difficultyRiseCount;

    private void Awake()
    {
        player = GameObject.FindWithTag(Strings.PlayerTag).GetComponent<PlayerController>();
        zombieMask = LayerMask.GetMask(Strings.ZombieMask);
        difficultyRiseCount = timeForRiseDifficulty;
    }

    private void Start()
    {
        for (int i = 0; i < maxZombiesAlive; i++)
        {
            StartCoroutine(SpawnOneZombie());
        }
    }

    private void Update()
    {
        RiseDifficulty();
        SpawnerCountdown();
    }

    private void SpawnerCountdown()
    {
        spawnTime = Random.Range(minSpawntime, maxSpawntime + 1);
        spawnTimeCountFloat += Time.deltaTime;
        spawnTimeCountInt = (int)(spawnTimeCountFloat % 60);
        if (spawnTimeCountInt >= spawnTime)
        {
            if (Vector3.Distance(player.transform.position, transform.position) >= distanceToSpawn
                && zombiesAlive < maxZombiesAlive)
            {
                zombiesAlive++;
                StartCoroutine(SpawnOneZombie());
            }
            spawnTimeCountFloat = 0;
        }
    }

    private IEnumerator SpawnOneZombie()
    {

        Vector3 spawnPos = RandomizePos();
        Collider[] enemiesInArea = Physics.OverlapSphere(spawnPos, spawnRadius, zombieMask);

        while (enemiesInArea.Length > 0)
        {
            spawnPos = RandomizePos();
            enemiesInArea = Physics.OverlapSphere(spawnPos, spawnRadius, zombieMask);
            yield return null;
        }

        Instantiate(zombiePref, spawnPos, transform.rotation)
        .GetComponent<ZombieController>().MySpawner = this;
    }

    private Vector3 RandomizePos()
    {
        Vector3 spawnPos = Random.insideUnitSphere * spawnRadius;
        spawnPos += transform.position;
        spawnPos.y = transform.position.y;
        return spawnPos;
    }

    private void RiseDifficulty()
    {
        if (Time.timeSinceLevelLoad >= difficultyRiseCount)
        {
            maxZombiesAlive++;
            difficultyRiseCount = Time.timeSinceLevelLoad + timeForRiseDifficulty;
        }
    }

    public void DecreaseZombiesAlive()
    {
        zombiesAlive--;
        if (zombiesAlive < 0)
            zombiesAlive = 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
