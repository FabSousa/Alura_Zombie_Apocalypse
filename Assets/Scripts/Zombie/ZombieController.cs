using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ZombieMoveAndRotate))]
[RequireComponent(typeof(Stats))]
public class ZombieController : MonoBehaviour, IDamageable
{
    [Header("instances")]
    private PlayerController player;
    private ZombieMoveAndRotate mr;
    private ZombieAnimationController ac;
    private Stats st;
    private UiController ui;
    public ZombieSpawner MySpawner {get; set;}
    [SerializeField] private ParticleSystem bloodParticle;

    [Header("Skins")]
    [SerializeField] private GameObject[] skins;

    [Header("Audio")]
    [SerializeField] private AudioClip dieSound;

    [Header("Attack")]
    private float attackRangeOffset = 0.5f;
    private float attackRange;
    [SerializeField][Min(0)] private int damage = 30;

    [Header("Drops")]
    [SerializeField] private MedKit medKitPref;
    private float medKitdropChance = 0.1f;

    [Header("Despawn")]
    [SerializeField][Min(0)] private float timeToDespawnSec = 2;

    private void Awake(){
        mr = GetComponent<ZombieMoveAndRotate>();
        ac = GetComponent<ZombieAnimationController>();
        player = GameObject.FindWithTag(Strings.PlayerTag).GetComponent<PlayerController>();
        st = GetComponent<Stats>();
        ui = GameObject.FindObjectOfType(typeof(UiController)) as UiController;
    }

    private void Start(){
        RandomizeSkin();
    }

    private void FixedUpdate(){
        mr.Move(st.Speed, out attackRange);
        mr.Rotate();
    }

    private void RandomizeSkin(){
        transform.GetChild(Random.Range(1, skins.Length)).gameObject.SetActive(true);
    }

    public void DoDamage(){
        if(mr.Dist <= attackRange+attackRangeOffset)
            player.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        st.CurrentHealth -= damage;
        if(st.CurrentHealth <= 0){
            st.CurrentHealth = 0;
            Die();
        }
    }

    public void InstantiateBloodParticle(Vector3 position, Quaternion rotation){
        Instantiate(bloodParticle, position, rotation);
    }

    public void Die()
    {
        MedKitDrop();
        MySpawner.DecreaseZombiesAlive();
        ui.UpdateKillCount();
        AudioController.instance.PlayOneShot(dieSound);
        ac.Die();
        StartCoroutine(mr.ClipThougthTheGround(timeToDespawnSec));
        Destroy(gameObject, timeToDespawnSec+1);
        this.enabled = false;
    }

    private void MedKitDrop(){
        if(Random.value <= medKitdropChance)
            Instantiate(medKitPref, transform.position, Quaternion.identity);
    }
}
