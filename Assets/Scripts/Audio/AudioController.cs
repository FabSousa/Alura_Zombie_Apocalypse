using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    public static AudioSource instance;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
        instance = audioSource;
    }
}
