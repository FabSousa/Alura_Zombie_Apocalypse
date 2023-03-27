using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMoveAndRotate))]
[RequireComponent(typeof(Stats))]
public class PlayerController : MonoBehaviour, IDamageable
{
    [Header("Instances")]
    [SerializeField] private Transform bulletSpawnSpot;
    [SerializeField] private GameObject bullet;
    private GameMode gm;
    private UiController ui;
    private PlayerMoveAndRotate mr;
    private Stats st;

    [Header("Audio")]
    [SerializeField] private AudioClip takeDamageSound;
    [SerializeField] private AudioClip shootSound;

    //[Header("Movment")]
    //public Vector3 Dir { get; private set; }

    //[Header("Health")]
    private Boolean isDead = false;

    private void Awake()
    {
        gm = GameObject.FindWithTag(Strings.CoreScriptsTag).GetComponent<GameMode>();
        ui = GameObject.FindWithTag(Strings.UiTag).GetComponent<UiController>();
        mr = GetComponent<PlayerMoveAndRotate>();
        st = GetComponent<Stats>();
    }

    private void Update()
    {
        ReadInputs();
    }

    private void FixedUpdate()
    {
        mr.Move(st.Speed);
        mr.Rotate();
    }

    private void ReadInputs(){
        if (Input.GetButtonDown("Fire1"))
        {
            if (isDead) gm.Restart();
            else Shoot();
        }
    }

    private void Shoot()
    {
        AudioController.instance.PlayOneShot(shootSound);
        Instantiate(bullet, bulletSpawnSpot.position, bulletSpawnSpot.rotation);
    }

    public void TakeDamage(int damage)
    {
        st.CurrentHealth -= damage;
        ui.UpdatePlayerHealthSlider();
        AudioController.instance.PlayOneShot(takeDamageSound);
        if (st.CurrentHealth <= 0)
        {
            st.CurrentHealth = 0;
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        gm.GameOver();
    }
    
}
