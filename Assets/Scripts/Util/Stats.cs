using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float MaxHealth = 100;
    [HideInInspector] public float CurrentHealth;
    [Min(0)] public float Speed = 1;

    void Start()
    {
        CurrentHealth = MaxHealth;
    }
}
