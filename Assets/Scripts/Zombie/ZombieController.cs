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
    private Stats st;
    private UiController ui;
    public ZombieSpawner MySpawner {get; set;}

    [Header("Audio")]
    [SerializeField] private AudioClip dieSound;

    [Header("Attack")]
    private float attackRangeOffset = 0.5f;
    private float attackRange;
    [SerializeField][Min(0)] private int damage = 30;

    [Header("Drops")]
    [SerializeField] private MedKit medKitPref;
    private float medKitdropChance = 0.1f;

    private void Awake(){
        mr = GetComponent<ZombieMoveAndRotate>();
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
        transform.GetChild(Random.Range(1, 28)).gameObject.SetActive(true);
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

    public void Die()
    {
        MedKitDrop();
        MySpawner.DecreaseZombiesAlive();
        ui.UpdateKillCount();
        AudioController.instance.PlayOneShot(dieSound);
        Destroy(gameObject);
    }

    private void MedKitDrop(){
        if(Random.value <= medKitdropChance)
            Instantiate(medKitPref, transform.position, Quaternion.identity);
    }
}
