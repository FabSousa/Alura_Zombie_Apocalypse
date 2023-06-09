using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedKit : MonoBehaviour
{
    private float lifeTime = 5;
    private int minHeal = 10;
    private int maxHeal = 20;
    private int healValue;

    private void Start(){
        healValue = (int)Random.Range(minHeal, maxHeal);
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other){
        if(other.tag.Equals(Strings.PlayerTag)){
            other.GetComponent<PlayerController>().Heal(healValue);
            Destroy(gameObject);
        }
    }
}
